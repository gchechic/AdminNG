using AdminNG.DAL;
using AdminNG.Models.CtaCte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AdminNG.Business
{
    public class BSMora : BSBase
    {
        public BSMora(AdminNGContext db)
        { 
            this.db = db;       
        }

        public List<CargoMora> GetCargoMorasMes(int intMes)
        {            
            DateTime dtFechaCalculo = new DateTime( DateTime.Today.Year, intMes , 1); // primero del mes actual
            List<CargoMora> listMoras = new List<CargoMora>();
            // cuotas impagas del mes anterior
            BSCuota bsCuota = new BSCuota(db);
            bsCuota.GetCuotasImpagas().Where(c => c.Mes <= intMes - 1).ToList().ForEach(cu => listMoras.Add(GetCargoMoraCuota(cu, dtFechaCalculo)));
           // db.CargoCuotas.Where(c => c.Saldo != 0 && c.Mes  <= intMes-1 ).ToList().ForEach(  cu => listMoras.Add(GetCargoMoraCuota(cu,dtFechaCalculo)));
            return listMoras;
        }
        public double MorasImporteCalculado(int FamiliaID, DateTime FechaCalculo)
        {
            double result = 0;

            /*
             * Buscar cuotas impagas para la familia. Impago significa que la diferencia entre el total y el saldo sea menor que la tolerancia
             * Para cada ouota impaga:
             *      Ver si ya existe Mora para este mes. Si existe devolver cero
             *      Con los datos de la cuota buscar el vencimiento
             *      Analizar si corresponde aplicar mora
             *      Si corresponde, buscar el codigo de valor
             * 
             */
            var cuotasImpagas = new BSCuota(db).GetCuotasImpagas(FamiliaID).ToList();

            foreach (var cuota in cuotasImpagas)
            {
                //Calcular Importe
                result += CalcularMoraImporte(cuota, FechaCalculo);
            }

            return result;
        }
        /// <summary>
        /// Calcular importe de la Mora correspondiente a la cuota
        /// </summary>
        /// <param name="cuota"></param>
        /// <param name="FechaCalculo"></param>
        /// <returns></returns>
        public double CalcularMoraImporte(CargoCuota cuota, DateTime FechaCalculo)
        {
            CargoMora mora = GetCargoMoraCuota(cuota, FechaCalculo);
            return mora == null ? 0 : mora.Importe;
        }
        public CargoMora GetCargoMoraCuota(CargoCuota cuota, DateTime FechaCalculo)
        {
            int mes = FechaCalculo.Month;
            int codigoValorID = 0;
            if (cuota.Mes < mes)
                codigoValorID = (int)AdminNG.Models.CargoCodigoValor.IDS.Mora2;
            else
                codigoValorID = MoraCargoCodigoID(BSCalendarioVto.getCalentarioVtoItem(cuota.InscripcionID, cuota.Mes), FechaCalculo);// 0 o mora 1 o mora 2

            if (codigoValorID == 0 || ExisteCargoMoraCuotaMes(cuota, mes))
                return null;
            AdminNG.Models.Inscripcion inscripcion = db.Inscripciones.Find(cuota.InscripcionID);
            double valor = BSCargoValor.GetValor(inscripcion.CursoID, codigoValorID, FechaCalculo);
            CargoMora mora = new CargoMora
            {
                CuotaID = cuota.ID,
                FamiliaID = cuota.FamiliaID,
                InscripcionID = cuota.InscripcionID,
                Fecha = FechaCalculo,
                Mes = mes,
                CargoCodigoValorID = codigoValorID,
                Importe = valor,
                Saldo = valor,
                FechaAlta = DateTime.Now,
                Usuario = AdminNG.Helpers.Configuracion.UsuarioSistema
            };
            return mora;
        }
        public static int MoraCargoCodigoID(Models.CalendarioVtoItem itemVto, DateTime FechaCalculo)
        {
            if (itemVto == null)
                return 0;
            if (FechaCalculo <= itemVto.PrimerVto)
                return 0;
            else if (FechaCalculo <= itemVto.SegundoVto)
                return (int)AdminNG.Models.CargoCodigoValor.IDS.Mora1;
            else
                return (int)AdminNG.Models.CargoCodigoValor.IDS.Mora2;
        }

        // cargo mora aplicado a la cuota para el mes especificado
        public bool ExisteCargoMoraCuotaMes(CargoCuota Cuota, int Mes)
        {
            return db.CargoMoras.Any(m => m.CuotaID == Cuota.ID && m.Mes == Mes);
        }
    }
}