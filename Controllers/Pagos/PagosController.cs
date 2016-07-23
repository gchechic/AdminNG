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

namespace AdminNG.Controllers
{
    public class  PagosController : Controller
    {
        private AdminNGContext db = new AdminNGContext();
        private AdminNG.Business.BSPago BSPago;
        private DBResponsable _dbResponsable = new DBResponsable();

        public PagosController()
        {
            BSPago = new Business.BSPago();
        }
        // GET: Pagos
        public async Task<ActionResult> Index()
        {
            var pagos = db.Pagos.Include(p => p.Familia).Include(p => p.FormaPago).Include(p => p.Responsable).Include(p => p.Sede);
            var l = pagos.ToList();
            return View(await pagos.ToListAsync());
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
           
           
            //pago.ComprobanteNumero = BSPago.PagoComprobanteNumeroUltimo(ResponsableNuevoID) + 1;
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
            if (ModelState.IsValid)
            {
                ///TODO:Validar
                // que no exista el nuevo nro

                //Se crea un pago para anular
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
                nuevoPago.Observaciones = string.Format("Renumerado a: Responsable: {0} Numero: {1}", pago.Responsable.Nombre, pago.ComprobanteNumero);
                nuevoPago.Anulado = true;

                //Cambios al pago actual
                pago.ResponsableID = vmpago.ResponsableNuevoID;
                pago.ComprobanteNumero = vmpago.NumeroNuevo;

                db.Pagos.Add(nuevoPago);
                db.Entry(pago).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            vmpago.Pago = pago;
            ViewBag.ResponsableNuevoID = new SelectList(responsables, "ID", "Nombre", vmpago.ResponsableNuevoID);               
            return View(vmpago);
        }
        // GET: Pagos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = await db.Pagos.FindAsync(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            return View(pago);
        }

        // POST: Pagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Pago pago = await db.Pagos.FindAsync(id);
            db.Pagos.Remove(pago);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

       
       
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
            if (pago.Anulado)
                return View(pago);
            int FormaPagoid = pago.FormaPagoID;
            switch (FormaPagoid)
            {
                case (int) FormaPago.IDS.Contado:
                    return RedirectToAction("Details", "PagoContados", new {id = id});
                case (int) FormaPago.IDS.Bancario:
                    return RedirectToAction("Details", "PagoBancarios", new { id = id });
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
    }
}
