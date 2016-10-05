using AdminNG.DAL;
using AdminNG.Helpers;
using AdminNG.Models;
using AdminNG.Models.CtaCte;
using AdminNG.Models.Pagos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AdminNG.Business
{
    public class BSMovimientoCuenta:BSBase
    {
        private DBMovimientoCargo dbMovimientoCargo;
        public BSMovimientoCuenta(AdminNGContext db)
        { 
            this.db = db;
            dbMovimientoCargo = new DBMovimientoCargo(db);
        }

         public  double  Saldo( int FamiliaID)
        {
            return dbMovimientoCargo.CargosPendientes(FamiliaID).Sum(c => c.Saldo);
        }

         //public static Models.Pagos.Pago GetMovimientoPago(int intFamiliaID)
         //{
         //    Models.Pagos.Pago movimientoPago = new Models.Pagos.Pago();
            
         //   movimientoPago.FamiliaID = intFamiliaID;
         //   movimientoPago.MovimientoCuentaTipoID = (int)AdminNG.Models.CtaCte.MovimientoCuentaTipo.IDS.Pago;
         //   movimientoPago.FechaAlta = DateTime.Now;
         //   return movimientoPago;
         //}

         public void ImputarMovimientos()
         {
            db.MovimientoCuentas.Where(m => m.Saldo < 0).OrderBy(m=>m.Fecha).ThenBy(m=>m.ID).ToList().ForEach(m =>ImputarMovimientos(m));//negativos
         }
         public void ImputarMovimientos(MovimientoCuenta  movimientoHaber)
         {
             int i = 0;
             double importe;
             MovimientoCuenta movimientoDebe;
            
             var movimientos = db.MovimientoCuentas.Where(m => m.Saldo > 0 && m.FamiliaID == movimientoHaber.FamiliaID).OrderBy(m => m.Fecha).ThenBy(m => m.ID).ToList();//positivos
             bool bFin = movimientos.Count == 0;
             if (bFin)
                 return;
            
             while (!bFin)
             {
                 //imputar
                 movimientoDebe = movimientos[i];
                 importe = System.Math.Min(movimientoDebe.Saldo, movimientoHaber.Saldo * -1);

                 movimientoDebe.Saldo -= importe;
                 movimientoHaber.Saldo += importe;

                 db.Entry(movimientoDebe).State = EntityState.Modified;
                 db.Imputaciones.Add(CrearImputacion(movimientoDebe, movimientoHaber,importe));                
                 bFin = movimientoHaber.Saldo == 0 || i++ == movimientos.Count - 1;
             }
             db.Entry(movimientoHaber).State = EntityState.Modified;
             db.SaveChanges();
         }

         public Imputacion CrearImputacion(MovimientoCuenta MovimientoDebe, MovimientoCuenta MovimientoHaber, double importe)
         {
             Imputacion imputacion = new Imputacion();
             imputacion.MovimientoDebe = MovimientoDebe;
             imputacion.MovimientoHaber = MovimientoHaber;
             imputacion.Importe = importe;
             db.Imputaciones.Add(imputacion);
             imputacion.Fecha = DateTime.Now;
             return imputacion;
         }
    }
}