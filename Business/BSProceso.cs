using AdminNG.DAL;
using AdminNG.Models.CtaCte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.Business
{
    public class BSProceso : BSBase
    {
        BSMora bsMora;
        BSCuota bsCuota;
        BSComedor bsComedor;
        BSMovimientoCuenta bsMovimientoCuenta;
        public BSProceso(AdminNGContext db)
        {
            this.db = db;
            bsMora = new BSMora(db);
            bsCuota = new BSCuota(db);
            bsComedor = new BSComedor(db);
            bsMovimientoCuenta = new BSMovimientoCuenta(db);
        }
        public void ProcesarMes( int Mes)
        {           
            List<CargoMora> listMoras= bsMora.GetCargoMorasMes(Mes ); //Generar moras para cuotas de meses anteriores con mes actual
            List<CargoCuota> listCuotas = bsCuota.GetCargoCuotasMes(Mes); //Generar cuotas del mes
            List<CargoComedor> listComedores = bsComedor.GetCargoComedoresMes(Mes); //Generar comedores vigentes al mes anterior con mes actual
            db.CargoMoras.AddRange(listMoras);
            db.CargoCuotas.AddRange(listCuotas);
            db.CargoComedores.AddRange(listComedores);
            db.SaveChanges();
            //imputar cargos por si quedaron pagos a cuenta u otros cargos negativos
            bsMovimientoCuenta.ImputarMovimientos();
        }
    }
}