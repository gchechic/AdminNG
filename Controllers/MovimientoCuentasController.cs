using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminNG.DAL;
using AdminNG.Models.CtaCte;

namespace AdminNG.Controllers
{
    public class MovimientoCuentasController : Controller
    {
        private AdminNGContext db = new AdminNGContext();

        // GET: MovimientoCuentas
        public ActionResult Index()
        {
            var movimientoCuentas = db.MovimientoCuentas.Include(m => m.Familia).Include(m => m.MovimientoCuentaTipo);
            return View(movimientoCuentas.ToList());
        }

        // GET: MovimientoCuentas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimientoCuenta movimientoCuenta = db.MovimientoCuentas.Find(id);
            if (movimientoCuenta == null)
            {
                return HttpNotFound();
            }
            return View(movimientoCuenta);
        }

        // GET: MovimientoCuentas/Create
        public ActionResult Create()
        {
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion");
            ViewBag.MovimientoCuentaTipoID = new SelectList(db.MovimientoCuentaTipos, "ID", "Descripcion");
            return View();
        }

        // POST: MovimientoCuentas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MovimientoCuentaTipoID,FamiliaID,Fecha,Importe,Saldo,Observaciones,FechaAlta,UsuarioNombre,x")] MovimientoCuenta movimientoCuenta)
        {
            if (ModelState.IsValid)
            {
                db.MovimientoCuentas.Add(movimientoCuenta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", movimientoCuenta.FamiliaID);
            ViewBag.MovimientoCuentaTipoID = new SelectList(db.MovimientoCuentaTipos, "ID", "Descripcion", movimientoCuenta.MovimientoCuentaTipoID);
            return View(movimientoCuenta);
        }

        // GET: MovimientoCuentas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimientoCuenta movimientoCuenta = db.MovimientoCuentas.Find(id);
            if (movimientoCuenta == null)
            {
                return HttpNotFound();
            }
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", movimientoCuenta.FamiliaID);
            ViewBag.MovimientoCuentaTipoID = new SelectList(db.MovimientoCuentaTipos, "ID", "Descripcion", movimientoCuenta.MovimientoCuentaTipoID);
            return View(movimientoCuenta);
        }

        // POST: MovimientoCuentas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MovimientoCuentaTipoID,FamiliaID,Fecha,Importe,Saldo,Observaciones,FechaAlta,UsuarioNombre,x")] MovimientoCuenta movimientoCuenta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movimientoCuenta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", movimientoCuenta.FamiliaID);
            ViewBag.MovimientoCuentaTipoID = new SelectList(db.MovimientoCuentaTipos, "ID", "Descripcion", movimientoCuenta.MovimientoCuentaTipoID);
            return View(movimientoCuenta);
        }

        // GET: MovimientoCuentas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimientoCuenta movimientoCuenta = db.MovimientoCuentas.Find(id);
            if (movimientoCuenta == null)
            {
                return HttpNotFound();
            }
            return View(movimientoCuenta);
        }

        // POST: MovimientoCuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovimientoCuenta movimientoCuenta = db.MovimientoCuentas.Find(id);
            db.MovimientoCuentas.Remove(movimientoCuenta);
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
