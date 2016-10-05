using AdminNG.DAL;
using AdminNG.Helpers;
using AdminNG.Models;
using AdminNG.Models.CtaCte;
using AdminNG.Models.Pagos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AdminNG.Business
{
    public class BSPago
    {
        private AdminNGContext db ;
        //private string strUsuario = System.Web.HttpContext.Current.User.Identity.Name;

        public BSPago(AdminNGContext db )
        {
            this.db = db;
        }
        public void  InicializarPago( AdminNG.Models.Pagos.Pago pago)
        {
            pago.Fecha = DateTime.Now;
            pago.Familia = db.Familia.Find(pago.FamiliaID);
            if (pago.ResponsableID != 0)
             pago.ComprobanteNumero = PagoComprobanteNumeroUltimo(pago.ResponsableID) + 1;
            AdminNG.Business.BSMovimientoCuenta bsMovimientoCuenta = new BSMovimientoCuenta(db);
            var saldo = bsMovimientoCuenta.Saldo(pago.FamiliaID);
            BSGtoAdm bsGtoAdm = new BSGtoAdm(db);
           saldo += bsGtoAdm.GtoAdmsImporteCalculado(pago.FamiliaID, pago.Fecha);
            if (saldo >= 0)
                pago.Importe = saldo;
        }

        public void InicializarPagoBancario(AdminNG.Models.Pagos.PagoBancario pago)
        {
            pago.ResponsableID = PagoResponsableIDInicial(pago.FamiliaID);
            pago.FechaFactura = DateTime.Today;
            InicializarPago(pago);
        }

        public void InicializarPagoContado(AdminNG.Models.Pagos.PagoContado pago)
        {
            pago.ResponsableID = db.Sedes.Find(pago.SedeID).ResponsableID;            
            InicializarPago(pago);
            pago.ImporteEfectivo = pago.Importe;
        }

        public async Task<bool> CrearPagoContado(AdminNG.Models.Pagos.PagoContado pagoContado, System.Web.Mvc.ModelStateDictionary ModelState)
        {
            bool bResult= false;

            // Esto debe hacerse antes de validar para evitar los duplicados
            pagoContado.ResponsableID = db.Sedes.Find(pagoContado.SedeID).ResponsableID;

            if (ValidarPagoContado(pagoContado, ModelState))
            {
                //Datos Pago
                //pagoContado.FormaPagoID = (int)FormaPago.IDS.Contado;
                pagoContado.Importe = pagoContado.ImporteEfectivo + pagoContado.ImporteCheques;

                pagoContado.FechaAlta = DateTime.Now;
                pagoContado.Importe = pagoContado.Importe * -1; //Signo Negativos
                pagoContado.Saldo = pagoContado.Importe;

                if (ModelState.IsValid)
                {
                    //db.PagoContados.Add(pagoContado);               
                    //await db.SaveChangesAsync();
                    //Task task = new Task(() => ProcesarPago(pagoContado));
                    //await task;
                    ProcesarPago(pagoContado);
                    bResult = true;
                }
            }
            return bResult;

        }

        public async Task<bool> CrearPagoBancario(PagoBancario pagoBancario, System.Web.Mvc.ModelStateDictionary ModelState)
        {
            bool bResult = false;

            if (ValidarPagoBancario(pagoBancario, ModelState))
            {
                //Datos Pago
                // pagoBancario.FormaPagoID = (int)FormaPago.IDS.Bancario;

                pagoBancario.FechaAlta = DateTime.Now;
                pagoBancario.Importe = pagoBancario.Importe * -1; //Signo Negativos                 
                pagoBancario.Saldo = pagoBancario.Importe;

                if (ModelState.IsValid)
                {                  
                    ProcesarPago(pagoBancario);
                    bResult = true;
                }
            }

            return bResult;

        }
        public bool ValidarPago(Models.Pagos.Pago pago, System.Web.Mvc.ModelStateDictionary ModelState)
        {
            bool bValido = true;
            if (pago.Fecha > DateTime.Today)
            {
                ModelState.AddModelError("Fecha", "La fecha no puede ser mayor a la actual");
                bValido = false;
            }
            DateTime? dt=  FechaUltimoPago(pago.FamiliaID);
            if ( dt.HasValue && pago.Fecha < dt)
            {
                ModelState.AddModelError("Fecha", "La fecha no puede ser menor a la del ultimo pago registrado:" + dt.Value.ToString("d"));
                bValido = false;
            }
           
            if (db.Pagos.Any( p => p.ResponsableID == pago.ResponsableID && p.ComprobanteNumero == pago.ComprobanteNumero))
            {
                ModelState.AddModelError("ComprobanteNumero", "numero existente");
                bValido = false;
            }
            return bValido;
        }
        public bool ValidarPagoContado(PagoContado pagoContado, System.Web.Mvc.ModelStateDictionary ModelState)
        {
            bool bValido = ValidarPago( pagoContado,ModelState);
            //validar
            if ( pagoContado.ImporteEfectivo < 0 )
            { 
                ModelState.AddModelError("ImporteEfectivo", "debe ser mayor a cero");
                bValido = false;
            }
            if (pagoContado.ImporteCheques < 0)
            { 
                ModelState.AddModelError("ImporteCheques", "debe ser mayor a cero");
                bValido = false;
            }
            if (pagoContado.ImporteEfectivo == 0 && pagoContado.ImporteCheques == 0)
            {
                ModelState.AddModelError("", "el importe contado o cheques debe ser mayor a cero");
                bValido = false;
            }
            if (pagoContado.ImporteCheques > 0 && string.IsNullOrEmpty(pagoContado.DetalleCheques ))
            {
                ModelState.AddModelError("DetalleCheques", "debe ingresar el detalle de los cheques");
                bValido = false;
            }
          
            return bValido;
        }

        public bool ValidarPagoBancario(PagoBancario pagoBancario, System.Web.Mvc.ModelStateDictionary ModelState)
        {
            bool bValido = ValidarPago(pagoBancario, ModelState);
            //validar
            if (pagoBancario.Importe <= 0)
            {
                ModelState.AddModelError("Importe", "debe ser mayor a cero");
                bValido = false;
            }
            return bValido;
        }

       

        public DateTime? FechaUltimoPago(int FamiliaID)
        {
            var n = (from p in db.Pagos
                     where p.FamiliaID == FamiliaID
                     select p.Fecha).DefaultIfEmpty().Max();

            return n;
        }
        public int PagoComprobanteNumeroUltimo(int ResponsableID)
        {
            var n = (from p in db.Pagos
                     where p.ResponsableID == ResponsableID
                     select p.ComprobanteNumero).DefaultIfEmpty().Max();
            
            return n;
        }
        public int PagoResponsableIDInicial( int FamiliaID)
        {
            var id= (from p in db.Pagos
                     where p.FamiliaID == FamiliaID
                     && p.Responsable.Grade != Grade.R
                     select p.ID).DefaultIfEmpty().Max();
            if (id > 0)
                return db.Pagos.Find(id).ResponsableID;
            return id;
        }

        // se buscan cargos con saldo ordenados por fecha, id desc
        // Mientras haya saldo en el pago
        // para cada cargo
        //      imputar
        //      si es cuota analizar si corresponde generar nuevo cargo para gtoadm
        //fin para
        // fin mientras
        // Mientras haya saldo en el pago
        // imputar gtoadms generadas en el paso anterior
        // FMientras
        public async void ProcesarPago(Pago pago)
        {
            DBMovimientoCargo dbMovimientoCargo = new DBMovimientoCargo(db);
            BSMovimientoCuenta bsMovimientoCuenta = new BSMovimientoCuenta(db);
            BSGtoAdm bsGtoAdm = new BSGtoAdm(db);
            var cargos = dbMovimientoCargo.CargosPendientes(pago.FamiliaID).Where(c => c.Saldo > 0).ToList();//positivos
            bool bFin = cargos.Count == 0;
            int i = 0;
            double importe, saldoOriginal;
            MovimientoCargo cargo;
            List<CargoGtoAdm> listGtoAdms = new List<CargoGtoAdm>();
            db.Pagos.Add(pago);
            while (!bFin)
            {
                //imputar
                cargo = cargos[i];
                importe = System.Math.Min(cargo.Saldo, pago.Saldo * -1);
                saldoOriginal = cargo.Saldo;
                cargo.Saldo -= importe;
                pago.Saldo += importe;                
                db.Imputaciones.Add(bsMovimientoCuenta.CrearImputacion(cargo, pago, importe)); 

                if (cargo.Importe != 0 && (cargo.GetType() == typeof(CargoCuota) || cargo.GetType().BaseType == typeof(CargoCuota)))
                {
                    CargoCuota cuota = cargo as CargoCuota;
                    // Calcular GtoAdm
                    // Si estaba impaga y dejo de estarlo calcular gtoadm , sino dejarlo para el proceso mensual
                    if ((cargo.Importe- saldoOriginal )  / cargo.Importe < Configuracion.PorcCuotaImpaga && !cuota.Impaga)
                    {                    
                        //Actualiza el importe y saldo del gto adm si ya existe o crea uno nuevo sino
                        CargoGtoAdm gtoadm = bsGtoAdm.SetCargoGtoAdmCuota(cargo as CargoCuota, pago.Fecha);
                        if (gtoadm != null)
                        {
                            if (gtoadm.ID == 0) // si es uno nuevo procesarlo al final si hay salod
                                listGtoAdms.Add(gtoadm);
                            else
                                db.Entry(gtoadm).State = EntityState.Modified;
                        }
                    }
                }
                db.Entry(cargo).State = EntityState.Modified;
                bFin = pago.Saldo == 0 || i++ == cargos.Count - 1;
            }
            //db.SaveChanges();
            // si se agregaron cargos de gtoadm y hay saldo hacer ciclo para imputarlos
            bFin = listGtoAdms.Count == 0 || pago.Saldo == 0;
            i = 0;
            while (!bFin)
            {
                //imputar
                CargoGtoAdm gtoadm = listGtoAdms[i];
                importe = System.Math.Min(gtoadm.Saldo, pago.Saldo * -1);
                gtoadm.Saldo -= importe;
                pago.Saldo += importe;
                db.Imputaciones.Add(bsMovimientoCuenta.CrearImputacion(gtoadm, pago, importe)); 
                bFin = pago.Saldo == 0 || i++ == listGtoAdms.Count - 1;
            }

            if (listGtoAdms.Count > 0)
                db.CargoGtoAdms.AddRange(listGtoAdms);
                            
             db.SaveChanges();
           
        }
    }
    
}