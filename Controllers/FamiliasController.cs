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
using AdminNG.ViewModels;
using AdminNG.Business;

namespace AdminNG.Controllers
{
    public class FamiliasController : Controller
    {
        private AdminNGContext db = new AdminNGContext();

        // GET: Familias
        public ActionResult Index()
        {
            return View(db.Familia.ToList());
        }

        // GET: Familias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familia familia = db.Familia.Find(id);
            if (familia == null)
            {
                return HttpNotFound();
            }
            return View(familia);
        }

        // GET: Familias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Familias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Descripcion")] Familia familia)
        {
            if (ModelState.IsValid)
            {
                db.Familia.Add(familia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(familia);
        }

        // GET: Familias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familia familia = db.Familia.Find(id);
            if (familia == null)
            {
                return HttpNotFound();
            }
            return View(familia);
        }

        // POST: Familias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Descripcion")] Familia familia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(familia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(familia);
        }

        // GET: Familias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familia familia = db.Familia.Find(id);
            if (familia == null)
            {
                return HttpNotFound();
            }
            return View(familia);
        }

        // POST: Familias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Familia familia = db.Familia.Find(id);
            db.Familia.Remove(familia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Ficha(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familia familia = db.Familia.Find(id);
            if (familia == null)
            {
                return HttpNotFound();
            }
            db.Entry(familia).Collection(f => f.Alumnos).Load();
            VMFicha vmFicha = new VMFicha();
            vmFicha.Familia = familia;
            BSMovimientoCuenta bsMovimientoCuenta = new BSMovimientoCuenta(db);
            vmFicha.Saldo = bsMovimientoCuenta.Saldo(id.Value);
            vmFicha.CargosPendientes = db.MovimientoCargos.Where(c => c.FamiliaID == id && c.Saldo > 0).OrderBy(c => c.Fecha).ThenBy(c => c.ID).ToList();
            return View(vmFicha);
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
