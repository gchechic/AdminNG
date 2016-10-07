using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminNG.DAL;
using AdminNG.Models;
using AdminNG.Models.Pagos;
using AdminNG.Helpers;
using System.Data.Entity.Infrastructure;
using AdminNG.Business;
using AdminNG.ViewModels;
using AdminNG.ViewModels.Pagos;

namespace AdminNG.Controllers
{
    public class  PagosController : Controller
    {
        private AdminNGContext db = new AdminNGContext();
        private AdminNG.Business.BSPago BSPago;
        private DBResponsable _dbResponsable = new DBResponsable();

        public PagosController()
        {
            BSPago = new Business.BSPago(db);
        }


        // GET: Pagos
        public async Task<ActionResult> Index()
        {
            var pagos = db.Pagos.Include(p => p.Familia).Include(p => p.FormaPago).Include(p => p.Responsable).Include(p => p.Sede);
            
            var l = pagos.ToList();
            //l.ForEach( p=> p.Importe *= -1); // cambiar signo
            return View(await pagos.ToListAsync());
        }


        public ActionResult Create(int FamiliaID)
        {
            VMPagoCrear newPago = new VMPagoCrear();
            newPago.FamiliaID = FamiliaID;
            newPago.Fecha = DateTime.Now;
            newPago.Familia = db.Familia.Find(newPago.FamiliaID);


            ViewBag.Saldo = new BSFicha(db).Saldo(newPago.FamiliaID, newPago.Fecha);
            return View(newPago);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMPagoCrear pagoContado)
        {
            try
            {
                AdminNG.Business.BSPago BSPago = new Business.BSPago(db);

                //pagoContado.Usuario = System.Web.HttpContext.Current.User.Identity.Name;
                //pagoContado.SedeID = (int)Session["SedeID"];

                //if (await BSPago.CrearPagoContado(pagoContado, ModelState))
                //{
                //    return RedirectToAction("Index", "Pagos");
                //}
                ////db.Entry(pagoContado).Reference( p => p.Familia ).Load();
                pagoContado.Familia = db.Familia.Find(pagoContado.FamiliaID);

            }
            catch (RetryLimitExceededException ex)
            {
                LogHelper.LogExcepcion(ex);
                ModelState.AddModelError("", AdminNG.Properties.Resources.UnableToSaveChanges);
            }
            //ViewBag.ID = new SelectList(db.Inscripciones, "ID", "ID", alumno.ID);
            return View(pagoContado);
        }

        // GET: Pagos/Renumerar/5
        public ActionResult Renumerar(int? id)
        {
            var responsables = _dbResponsable.GetResonsablesNoR();

             if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             Pago pago = db.Pagos.Find(id) ;
            if (pago == null)
            {
                return HttpNotFound();
            }
           
            ViewModels.VMPagoRenumerar vmPago = new ViewModels.VMPagoRenumerar();
            //Sugerir responsable y numero
            int ResponsableNuevoID = BSPago.PagoResponsableIDInicial(pago.FamiliaID);
            if (ResponsableNuevoID == 0)
                 responsables.Insert(0, null );
            else
                vmPago.NumeroNuevo = BSPago.PagoComprobanteNumeroUltimo(ResponsableNuevoID) + 1;
            ViewBag.ResponsableNuevoID = new SelectList(responsables, "ID", "Nombre", ResponsableNuevoID);
           
            vmPago.Pago = pago;
            
            return View(vmPago);
            
        }

        // POST: Pagos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Renumerar([Bind(Include = "ID,ResponsableNuevoID,NumeroNuevo")] ViewModels.VMPagoRenumerar vmpago)
        {
            var responsables = _dbResponsable.GetResonsablesNoR();
            Pago pago = db.Pagos.Find(vmpago.ID);
            if (pago == null)
            {
                return HttpNotFound();
            }

            // Validar que no exista el nuevo nro
            if (db.Pagos.Any(p => p.ResponsableID == vmpago.ResponsableNuevoID && p.ComprobanteNumero == vmpago.NumeroNuevo))
            {
                ModelState.AddModelError("NumeroNuevo", "numero existente");              
            }
            else if (ModelState.IsValid)
            {
                //Se crea un pago para anular copiando el anterior
                Pago nuevoPago = new PagoContado();
                //copiar todas las propiedades                
                nuevoPago.FamiliaID = pago.FamiliaID;
                nuevoPago.Fecha = pago.Fecha;
                nuevoPago.FormaPagoID = pago.FormaPagoID;
                nuevoPago.SedeID = pago.SedeID;
                nuevoPago.Importe = pago.Importe;
                nuevoPago.Observaciones = pago.Observaciones;
                nuevoPago.ResponsableID = pago.ResponsableID;
                nuevoPago.ComprobanteNumero = pago.ComprobanteNumero;
                // Seteo de propiedades para el nuevo pago
                nuevoPago.SedeID = (int)Session["SedeID"];
                nuevoPago.FechaAlta = DateTime.Now;
                nuevoPago.Usuario = System.Web.HttpContext.Current.User.Identity.Name;
                nuevoPago.Observaciones = string.Format("Renumerado a: Responsable: {0} Numero: {1}", db.Responsables.Find(vmpago.ResponsableNuevoID).Nombre, vmpago.NumeroNuevo);
                nuevoPago.Anulado = true;

                //Cambios al pago actual
                pago.ResponsableID = vmpago.ResponsableNuevoID;              
                pago.ComprobanteNumero = vmpago.NumeroNuevo;

              
                if (TryUpdateModel(pago, "", new string[] { "ResponsableID", "ComprobanteNumero" }))
                {
                    try
                    {
                        db.Pagos.Add(nuevoPago);
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    catch (RetryLimitExceededException ex)
                    {
                        LogHelper.LogExcepcion(ex);
                        ModelState.AddModelError("", AdminNG.Properties.Resources.UnableToSaveChanges);
                    }
                }
                //db.Entry(pago).State = EntityState.Modified;
                //db.SaveChanges();

                //return RedirectToAction("Index");
            }
            vmpago.Pago = pago;
            ViewBag.ResponsableNuevoID = new SelectList(responsables, "ID", "Nombre", vmpago.ResponsableNuevoID);               
            return View(vmpago);
        }

        //// GET: Pagos/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Pago pago = await db.Pagos.FindAsync(id);
        //    if (pago == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(pago);
        //}

        //// POST: Pagos/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Pago pago = await db.Pagos.FindAsync(id);
        //    db.Pagos.Remove(pago);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}
   
       
        // GET: Pagoss/Details/5/1

        public ActionResult Details(int? id)
        {
            if (id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = db.Pagos.Find(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            //if (pago.Anulado)
            //    return View(pago);
            int FormaPagoid = pago.FormaPagoID;
            switch (FormaPagoid)
            {
                case (int) FormaPago.IDS.Contado:
                    return RedirectToAction("Details", "PagoContados", new {id = id});
                case (int) FormaPago.IDS.Bancario:
                    return RedirectToAction("Details", "PagoBancarios", new { id = id });
            }

            return View(pago);        
        
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public int UltimoNumero(int ResponsableID)
        {
            return BSPago.PagoComprobanteNumeroUltimo(ResponsableID)+1;
        }

        public double Saldo(int FamiliaID, string strFecha)
        {
            DateTime Fecha = DateTime.Parse(strFecha);
            AdminNG.Business.BSFicha bsFicha = new BSFicha(db);
            var saldo = bsFicha.Saldo(FamiliaID, Fecha);
           
            return saldo;
        }
    }
}
