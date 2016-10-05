using AdminNG.DAL;
using AdminNG.Models.CtaCte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AdminNG.Business
{
    public class BSGtoAdm : BSBase
    {
        public BSGtoAdm(AdminNGContext db)
        { 
            this.db = db;       
        }

        public List<CargoGtoAdm> GetCargoGtoAdmsMes(int intMes)
        {            
            DateTime dtFechaCalculo = new DateTime( DateTime.Today.Year, intMes , 1).AddDays(-1); // ultimo dia del mes anterior
            List<CargoGtoAdm> listGtoAdms = new List<CargoGtoAdm>();
            // cuotas impagas del mes anterior
            BSCuota bsCuota = new BSCuota(db);
            bsCuota.GetCuotasImpagas().Where(c => c.Mes <= intMes - 1).ToList().ForEach(cu => listGtoAdms.Add(SetCargoGtoAdmCuota(cu, dtFechaCalculo)));
           // db.CargoCuotas.Where(c => c.Saldo != 0 && c.Mes  <= intMes-1 ).ToList().ForEach(  cu => listGtoAdms.Add(GetCargoGtoAdmCuota(cu,dtFechaCalculo)));
            return listGtoAdms;
        }
        public double GtoAdmsImporteCalculado(int FamiliaID, DateTime FechaCalculo)
        {
            double result = 0;

            /*
             * Buscar cuotas impagas para la familia. Impago significa que la diferencia entre el total y el saldo sea menor que la tolerancia
             * Para cada ouota impaga:
             *      Ver si ya existe GtoAdm para este mes. Si existe devolver cero
             *      Con los datos de la cuota buscar el vencimiento
             *      Analizar si corresponde aplicar gtoadm
             *      Si corresponde, buscar el codigo de valor
             * 
             */
            var cuotasImpagas = new BSCuota(db).GetCuotasImpagas(FamiliaID).ToList();

            foreach (var cuota in cuotasImpagas)
            {
                //Calcular Importe
                result += CalcularGtoAdmImporte(cuota.InscripcionID, FechaCalculo);
            }

            return result;
        }

        public int getGtoAdmCodigoValorID(int inscripcionID, DateTime FechaCalculo)
        {
            int mes = FechaCalculo.Month;
            int codigoValorID = GtoAdmCargoCodigoID(BSCalendarioVto.getCalendarioVtoItem(inscripcionID, mes), FechaCalculo);// 0 o gtoadm 1 o gtoadm 2
            return codigoValorID;    
        }
        /// <summary>
        /// Calcular importe de la GtoAdm correspondiente a la fecha
        /// </summary>
        /// <param name="cuota"></param>
        /// <param name="FechaCalculo"></param>
        /// <returns></returns>
        public double CalcularGtoAdmImporte(int inscripcionID , DateTime FechaCalculo)
        {
            double valor = 0;
            int codigoValorID = getGtoAdmCodigoValorID(inscripcionID, FechaCalculo);
            if (codigoValorID != 0)
            {
                AdminNG.Models.Inscripcion inscripcion = db.Inscripciones.Find(inscripcionID);
                 valor = BSCargoValor.GetValor(inscripcion.CursoID, codigoValorID, FechaCalculo);
            }
            return valor;
        }
        /// <summary>
        /// Calcular importe de la GtoAdm correspondiente a la cuota para la fecha del dia.
        /// El valor no depende de la fecha de la cuota sino de la fecha de calculo
        /// </summary>
        /// <param name="cuota"></param>
        /// <param name="FechaCalculo"></param>
        /// <returns></returns>
        public double CalcularGtoAdmImporte(CargoCuota cuota, DateTime FechaCalculo)
        {
            //int mes = FechaCalculo.Month;
            //int codigoValorID  = GtoAdmCargoCodigoID(BSCalendarioVto.getCalendarioVtoItem(cuota.InscripcionID, cuota.Mes), FechaCalculo);// 0 o gtoadm 1 o gtoadm 2
            AdminNG.Models.Inscripcion inscripcion = db.Inscripciones.Find(cuota.InscripcionID);
            double valor = CalcularGtoAdmImporte(inscripcion.ID, FechaCalculo);
            return valor;
        }

        /// <summary>
        /// Devuleve el gto Adm correspondiente a la cuota actualizado o null si no corresponde gtoAdm
        /// </summary>
        /// <param name="cuota"></param>
        /// <param name="FechaCalculo"></param>
        /// <returns></returns>
        public CargoGtoAdm SetCargoGtoAdmCuota(CargoCuota cuota, DateTime FechaCalculo)
        {
            int mes = FechaCalculo.Month;
            int codigoValorID = 0;
            //if (cuota.Mes < mes)
            //    codigoValorID = (int)AdminNG.Models.CargoCodigoValor.IDS.GtoAdm2;
            //else
            codigoValorID = getGtoAdmCodigoValorID(cuota.InscripcionID, FechaCalculo); // 0 o gtoadm 1 o gtoadm 2
            if (codigoValorID == 0)
                return null;
            AdminNG.Models.Inscripcion inscripcion = db.Inscripciones.Find(cuota.InscripcionID);
            CargoGtoAdm gtoadm = GetCargoGtoAdmCuota(cuota);
            // si el gto adm no existe crear uno
            if (gtoadm == null)
            {                              
                gtoadm = new CargoGtoAdm
                {
                    CuotaID = cuota.ID,
                    FamiliaID = cuota.FamiliaID,
                    InscripcionID = cuota.InscripcionID,
                    Fecha = FechaCalculo,
                    Mes = mes,
                    CargoCodigoValorID = codigoValorID,
                    //Importe = valor,
                    //Saldo = valor,
                    FechaAlta = DateTime.Now,
                    Usuario = AdminNG.Helpers.Configuracion.UsuarioSistema
                };
            }
            double valor = BSCargoValor.GetValor(inscripcion.CursoID, codigoValorID, FechaCalculo);
            gtoadm.Importe += valor;
            gtoadm.Saldo += valor;
            gtoadm.Cantidad++; // Cantidad de meses
            return gtoadm;
        }
        public static int GtoAdmCargoCodigoID(Models.CalendarioVtoItem itemVto, DateTime FechaCalculo)
        {
            if (itemVto == null)
                return 0;
            if (FechaCalculo <= itemVto.PrimerVto)
                return 0;
            else if (FechaCalculo <= itemVto.SegundoVto)
                return (int)AdminNG.Models.CargoCodigoValor.IDS.GtoAdm1;
            else
                return (int)AdminNG.Models.CargoCodigoValor.IDS.GtoAdm2;
        }

        // cargo gtoadm aplicado a la cuota para el mes especificado
        public bool ExisteCargoGtoAdmCuotaMes(CargoCuota Cuota, int Mes)
        {
            return db.CargoGtoAdms.Any(m => m.CuotaID == Cuota.ID && m.Mes == Mes);
        }
        public CargoGtoAdm GetCargoGtoAdmCuota(CargoCuota Cuota)
        {
            return db.CargoGtoAdms.Where(m => m.CuotaID == Cuota.ID ).FirstOrDefault();
        }
    }
}