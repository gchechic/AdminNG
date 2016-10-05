using AdminNG.DAL;
using AdminNG.Models;
using AdminNG.Models.CtaCte;
using AdminNG.Models.Pagos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AdminNG.Business
{
    public class Pago
    {
        private AdminNGContext db = new AdminNGContext();
        private string strUsuario = System.Web.HttpContext.Current.User.Identity.Name;

        public void  InicializarPago( AdminNG.Models.Pagos.Pago pago)
        {
            pago.Fecha = DateTime.Now;
            pago.Familia = db.Familia.Find(pago.FamiliaID);
            pago.ComprobanteNumero = PagoComprobanteNumeroUltimo(pago.ResponsableID) + 1;
            var saldo = AdminNG.Business.BSMovimientoCuenta.Saldo(pago.FamiliaID);
            saldo += AdminNG.Business.BSMovimientoCuenta.MorasImporteCalculado(pago.FamiliaID, pago.Fecha);
            if (saldo >= 0)
                pago.Importe = saldo;
        }

        public void InicializarPagoBancario(AdminNG.Models.Pagos.PagoBancario pago)
        {
            pago.ResponsableID = PagoResponsableIDInicial(pago.FamiliaID);
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
          //  Models.CtaCte.MovimientoPago movCuenta = BSMovimientoCuenta.GetMovimientoPago(pagoContado.FamiliaID);
            if (ValidarPagoContado(pagoContado, ModelState))
            {
                //Datos Pago
                pagoContado.FormaPagoID = (int)FormaPago.IDS.Contado;
                pagoContado.Importe = pagoContado.ImporteEfectivo + pagoContado.ImporteCheques;

                pagoContado.ResponsableID = db.Sedes.Find(pagoContado.SedeID).ResponsableID;


                ///TODO poner cuando este habilitado loggin                    
                pagoContado.Usuario = strUsuario;// User.Identity.Name;
                pagoContado.FechaAlta = DateTime.Now;
                //pagoContado.SedeID = (int)Session["SedeID"]; //sacarlo del usuario


                pagoContado.Importe = pagoContado.Importe * -1; //Signo Negativos
                pagoContado.Saldo = pagoContado.Importe;

            }

            if (ModelState.IsValid)
            {
                db.PagoContados.Add(pagoContado);
               
                await db.SaveChangesAsync();
                bResult= true;
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
            return bValido;
        }
        public bool ValidarPagoContado(PagoContado pagoContado, System.Web.Mvc.ModelStateDictionary ModelState)
        {
            bool bValido = ValidarPago( pagoContado,ModelState);
            //validar

            return bValido;
        }

        public bool ValidarPagoBancario(PagoBancario pagoBancario, System.Web.Mvc.ModelStateDictionary ModelState)
        {
            bool bValido = ValidarPago(pagoBancario, ModelState);
            //validar

            return bValido;
        }

        public async Task<bool> CrearPagoBancario(PagoBancario pagoBancario, System.Web.Mvc.ModelStateDictionary ModelState)
        {
            bool bResult = false;
            try
            {                
                pagoBancario.Usuario = strUsuario;// User.Identity.Name;
                pagoBancario.FechaAlta = DateTime.Now;
                //Models.CtaCte.MovimientoPago movCuenta = CrearMovimientoPago(pagoBancario);

                if (ValidarPagoBancario(pagoBancario, ModelState))
                {
                    //Datos Pago
                    pagoBancario.FormaPagoID = (int)FormaPago.IDS.Bancario;

                    ///TODO poner cuando este habilitado loggin                    
                }

                if (ModelState.IsValid)
                {
                    db.PagoBancarios.Add(pagoBancario);
                   // db.MovimientoCuentas.Add(movCuenta);

                    await db.SaveChangesAsync();
                    bResult = true;
                }


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
            return bResult;

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
        //private Models.Pagos.Pago CrearMovimientoPago(Models.Pagos.Pago pago)
        //{
        //    Models.CtaCte.MovimientoPago movCuenta = BSMovimientoCuenta.GetMovimientoPago(pago.FamiliaID);
        //    movCuenta.Fecha = pago.Fecha;
        //    movCuenta.FechaAlta = pago.FechaAlta;
        //    movCuenta.Importe = pago.Importe * -1; //Signo Negativos
        //    movCuenta.Saldo = movCuenta.Importe;
        //    movCuenta.UsuarioNombre = pago.UsuarioNombre;
        //    movCuenta.PagoID = pago.ID;
        //    return movCuenta;
        //}

    }
    
}