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
    public class PagoBancariosController : Controller
    {
        private AdminNGContext db = new AdminNGContext();
        private DBResponsable _repositorio = new DBResponsable();
        private Business.BSPago BSPago;

        public PagoBancariosController()
        {
            BSPago = new Business.BSPago();

        }
        // GET: PagoBancarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoBancario pagoBancario = db.PagoBancarios.Find(id);
            if (pagoBancario == null)
            {
                return HttpNotFound();
            }
            return View(pagoBancario);
        }

        // GET: PagoBancarios/Create
        public ActionResult Create( int FamiliaID)
        {
            var responsables = _repositorio.GetResonsablesNoR();
            responsables.Insert(0, null );
           
            //ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion");
            
            PagoBancario pagoBancario = new PagoBancario();
            pagoBancario.FamiliaID = FamiliaID;
            pagoBancario.SedeID = (int)Session["SedeID"]; //sacarlo del usuario
                        
            BSPago.InicializarPagoBancario(pagoBancario);

            //pagoBancario.Fecha = DateTime.Now;
            
     
          //  pagoBancario.ResponsableID = 0;
            ViewBag.ResponsableID = new SelectList(responsables, "ID", "Nombre", pagoBancario.ResponsableID );            
            return View(pagoBancario);   

        }

        // POST: PagoBancarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FamiliaID,ResponsableID,ComprobanteNumero,EsDeposito,Fecha,Importe,DetalleBancario,Observaciones")] PagoBancario pagoBancario)
        {
            AdminNG.Business.BSPago BSPago = new Business.BSPago();
            var responsables = _repositorio.GetResonsablesNoR();
            pagoBancario.SedeID = (int)Session["SedeID"]; //sacarlo del usuario
            if (await BSPago.CrearPagoBancario(pagoBancario, ModelState))
            {
                return RedirectToAction("Index","Pagos");
            }
            if (ModelState.IsValid)
            {
                db.Pagos.Add(pagoBancario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ResponsableID = new SelectList(responsables, "ID", "Nombre", pagoBancario.ResponsableID);            
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", pagoBancario.FamiliaID);
           
            return View(pagoBancario);
        }

        // GET: PagoBancarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoBancario pagoBancario = db.PagoBancarios.Find(id);
            if (pagoBancario == null)
            {
                return HttpNotFound();
            }
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", pagoBancario.FamiliaID);
            ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion", pagoBancario.FormaPagoID);
            ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre", pagoBancario.ResponsableID);
            ViewBag.SedeID = new SelectList(db.Sedes, "ID", "Nombre", pagoBancario.SedeID);
            return View(pagoBancario);
        }

        // POST: PagoBancarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FamiliaID,FormaPagoID,ResponsableID,SedeID,ComprobanteNumero,Fecha,Importe,Observaciones,Anulado,FechaAlta,UsuarioNombre")] PagoBancario pagoBancario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagoBancario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", pagoBancario.FamiliaID);
            ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion", pagoBancario.FormaPagoID);
            ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre", pagoBancario.ResponsableID);
            ViewBag.SedeID = new SelectList(db.Sedes, "ID", "Nombre", pagoBancario.SedeID);
            return View(pagoBancario);
        }

        // GET: PagoBancarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoBancario pagoBancario = db.PagoBancarios.Find(id);
            if (pagoBancario == null)
            {
                return HttpNotFound();
            }
            return View(pagoBancario);
        }

        // POST: PagoBancarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PagoBancario pagoBancario = db.PagoBancarios.Find(id);
            db.Pagos.Remove(pagoBancario);
            db.SaveChanges();
            return RedirectToAction("Index");
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
