using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminNG.DAL;
using AdminNG.Models;
using AdminNG.Models.Pagos;
using System.Threading.Tasks;

namespace AdminNG.Controllers
{
    public class PagoContadosController : Controller
    {
        private AdminNGContext db = new AdminNGContext();
        private AdminNG.Business.BSPago BSPago;
        public PagoContadosController()
        {
            BSPago = new Business.BSPago();
        }
        // GET: PagoContados
        public ActionResult Index()
        {
            var pagos = db.Pagos.Include(p => p.Familia).Include(p => p.FormaPago).Include(p => p.Responsable).Include(p => p.Sede);
            return View(pagos.ToList());
        }

        // GET: PagoContados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoContado pagoContado = db.PagoContados.Find(id);
            if (pagoContado == null)
            {
                return HttpNotFound();
            }
            return View(pagoContado);
        }


        // GET: PagoContados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoContado pagoContado = db.PagoContados.Find(id);
            if (pagoContado == null)
            {
                return HttpNotFound();
            }
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", pagoContado.FamiliaID);
            ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion", pagoContado.FormaPagoID);
            ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre", pagoContado.ResponsableID);
            ViewBag.SedeID = new SelectList(db.Sedes, "ID", "Nombre", pagoContado.SedeID);
            return View(pagoContado);
        }

        // POST: PagoContados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FamiliaID,FormaPagoID,ResponsableID,SedeID,ComprobanteNumero,Fecha,Importe,Observaciones,Anulado,FechaAlta,UsuarioNombre,ImporteEfectivo,ImporteCheques,DetalleCheques")] PagoContado pagoContado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagoContado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", pagoContado.FamiliaID);
            ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion", pagoContado.FormaPagoID);
            ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre", pagoContado.ResponsableID);
            ViewBag.SedeID = new SelectList(db.Sedes, "ID", "Nombre", pagoContado.SedeID);
            return View(pagoContado);
        }

        // GET: PagoContados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoContado pagoContado = db.PagoContados.Find(id);
            if (pagoContado == null)
            {
                return HttpNotFound();
            }
            return View(pagoContado);
        }

        // POST: PagoContados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PagoContado pagoContado = db.PagoContados.Find(id);
            db.Pagos.Remove(pagoContado);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreateRecibo(int FamiliaID)
        {
            //ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion");
            //ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre");
            PagoContado pagoContado = new PagoContado();
            pagoContado.FamiliaID = FamiliaID;
            pagoContado.SedeID = (int)Session["SedeID"]; 

            BSPago.InicializarPagoContado(pagoContado);

            return View(pagoContado);
        }

        // POST: Recibos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRecibo([Bind(Include = "FamiliaID,ComprobanteNumero,Fecha,Observaciones,ImporteEfectivo,ImporteCheques,DetalleCheques")] PagoContado pagoContado)
        {
            try
            {
                AdminNG.Business.BSPago BSPago = new Business.BSPago();

                pagoContado.Usuario = System.Web.HttpContext.Current.User.Identity.Name;
                pagoContado.SedeID = (int)Session["SedeID"]; 
                if (await BSPago.CrearPagoContado(pagoContado, ModelState))
                {
                    return RedirectToAction("Index","Pagos");
                }

                //ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", pagoContado.FamiliaID);
                //ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre", pagoContado.ResponsableID);
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //ViewBag.ID = new SelectList(db.Inscripciones, "ID", "ID", alumno.ID);
            return View(pagoContado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
