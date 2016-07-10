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
using AdminNG.Models.CtaCte;

namespace AdminNG.Controllers.Cargos
{
    public class CargoComedoresController : Controller
    {
        private AdminNGContext db = new AdminNGContext();

        // GET: CargoComedores
        public async Task<ActionResult> Index()
        {
            var cargoComedores = db.CargoComedores.Include(c => c.Familia).Include(c => c.MovimientoCuentaTipo);
            return View(await cargoComedores.ToListAsync());
        }

        // GET: CargoComedores/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoComedor cargoComedor = await db.CargoComedores.FindAsync(id);
            if (cargoComedor == null)
            {
                return HttpNotFound();
            }
            return View(cargoComedor);
        }

        // GET: CargoComedores/Create
        public ActionResult Create()
        {
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion");
            ViewBag.MovimientoCuentaTipoID = new SelectList(db.MovimientoCuentaTipos, "ID", "Descripcion");
            return View();
        }

        // POST: CargoComedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,MovimientoCuentaTipoID,FamiliaID,Fecha,Importe,Saldo,Observaciones,FechaAlta,UsuarioNombre,x,Mes")] CargoComedor cargoComedor)
        {
            if (ModelState.IsValid)
            {
                db.MovimientoCuentas.Add(cargoComedor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", cargoComedor.FamiliaID);
            ViewBag.MovimientoCuentaTipoID = new SelectList(db.MovimientoCuentaTipos, "ID", "Descripcion", cargoComedor.MovimientoCuentaTipoID);
            return View(cargoComedor);
        }

        // GET: CargoComedores/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoComedor cargoComedor = await db.CargoComedores.FindAsync(id);
            if (cargoComedor == null)
            {
                return HttpNotFound();
            }
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", cargoComedor.FamiliaID);
            ViewBag.MovimientoCuentaTipoID = new SelectList(db.MovimientoCuentaTipos, "ID", "Descripcion", cargoComedor.MovimientoCuentaTipoID);
            return View(cargoComedor);
        }

        // POST: CargoComedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,MovimientoCuentaTipoID,FamiliaID,Fecha,Importe,Saldo,Observaciones,FechaAlta,UsuarioNombre,x,Mes")] CargoComedor cargoComedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cargoComedor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", cargoComedor.FamiliaID);
            ViewBag.MovimientoCuentaTipoID = new SelectList(db.MovimientoCuentaTipos, "ID", "Descripcion", cargoComedor.MovimientoCuentaTipoID);
            return View(cargoComedor);
        }

        // GET: CargoComedores/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoComedor cargoComedor = await db.CargoComedores.FindAsync(id);
            if (cargoComedor == null)
            {
                return HttpNotFound();
            }
            return View(cargoComedor);
        }

        // POST: CargoComedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CargoComedor cargoComedor = await db.CargoComedores.FindAsync(id);
            db.MovimientoCuentas.Remove(cargoComedor);
            await db.SaveChangesAsync();
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
