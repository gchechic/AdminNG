using AdminNG.DAL;
using AdminNG.Models.CtaCte;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AdminNG.Business
{
    public class BSMovimientoCuenta
    {
        private static AdminNGContext db = new AdminNGContext();

        public static List<AdminNG.Models.CtaCte.MovimientoCuenta> CargosPendientes( int FamiliaID)
        {
            var q = from cargo in db.MovimientoCuentas
                    where 
                    cargo.FamiliaID == FamiliaID &&
                    cargo.Saldo != 0    
                    orderby cargo.Fecha ascending , cargo.ID ascending
                    select cargo ;
            //var q1 = db.Cargos.Where<AdminNG.Models.Cargo>(c => c.Inscripcion.Alumno.FamiliaID == FamiliaID && c.Saldo != 0).OrderBy(c => c.Fecha);
            return q.ToList<AdminNG.Models.CtaCte.MovimientoCuenta>();
        }
         public static double  Saldo( int FamiliaID)
        {
            return CargosPendientes(FamiliaID).Sum(c => c.Importe);
        }
         public static Models.Pagos.Pago GetMovimientoPago(int intFamiliaID)
         {
             Models.Pagos.Pago movimientoPago = new Models.Pagos.Pago();
            
            movimientoPago.FamiliaID = intFamiliaID;
            movimientoPago.MovimientoCuentaTipoID = (int)AdminNG.Models.CtaCte.MovimientoCuentaTipo.IDS.Pago;
            movimientoPago.FechaAlta = DateTime.Now;
            return movimientoPago;
         }
        public static double MorasImporteCalculado( int FamiliaID, DateTime FechaCalculo)
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
           var cuotasImpagas = GetCuotasImpagas(FamiliaID).OrderBy(c=>c.ID).ToList();

           foreach (var cuota in cuotasImpagas)
           {               
               
               //Calcular Importe
               result += MoraImporteCalculado(cuota, FechaCalculo);
               
           }

            return result;
        }
        public static double MoraImporteCalculado(MovimientoCargo cuota, DateTime FechaCalculo)
        {
            int mes = FechaCalculo.Month;
            var codigoValorID = MoraCargoCodigoID(CalendarioVto.getCalentarioVtoItem(cuota.Inscripcion.ID, cuota.Mes), FechaCalculo);// 0 o mora 1 o mora 2
            if (codigoValorID == 0)
                return 0;
            if (GetMoraCuotaMes(cuota, mes) != null)
                return 0;
            return CargoValor.GetValor(cuota.Inscripcion.CursoID, codigoValorID, FechaCalculo);
        }
        public static int MoraCargoCodigoID( Models.CalendarioVtoItem itemVto, DateTime FechaCalculo)
        {
            if (itemVto == null)
                return 0;
            if (FechaCalculo <= itemVto.PrimerVto)
                return 0;
            else if (FechaCalculo <= itemVto.SegundoVto)
                return (int)AdminNG.Models.CargoCodigoValor.CargoCodigoValorIDs.Mora1;
            else
                return (int)AdminNG.Models.CargoCodigoValor.CargoCodigoValorIDs.Mora2;
        }
        public static IEnumerable<CargoCuota> GetCuotasImpagas( int FamiliaID)
        {
            Single PorcCuotaImpaga = Single.Parse(WebConfigurationManager.AppSettings["PorcCuotaImpaga"]);
           var cuotasImpagas = db.CargoCuotas.Where(
                    m => m.FamiliaID == FamiliaID && 
                    //m.CargoTipoID== (int)CargoTipo.IDS.Cuota &&
                    m.Importe != 0 && m.Saldo / m.Importe <= PorcCuotaImpaga
                    );
           return cuotasImpagas;
        }

        // cargo mora aplicado a la cuota para el mes especificado
        public static CargoMora GetMoraCuotaMes(MovimientoCargo Cuota, int Mes)
        {
            return db.CargoMoras.Where(m => m.CuotaID == Cuota.ID && m.Mes == Mes).FirstOrDefault();
        }
    }
}