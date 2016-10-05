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

namespace AdminNG.Controllers
{
    public class MovimientoPagosController : Controller
    {
        private AdminNGContext db = new AdminNGContext();

        // GET: MovimientoPagos
        public async Task<ActionResult> Index()
        {
            var movimientoCuentas = db.MovimientoPagos.Include(m => m.Familia).Include(m => m.MovimientoCuentaTipo).Include(m => m.FormaPago).Include(m => m.Responsable).Include(m => m.Sede);
            return View(await movimientoCuentas.ToListAsync());
        }

        // GET: MovimientoPagos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimientoPago movimientoPago = await db.MovimientoPagos.FindAsync(id);
            if (movimientoPago == null)
            {
                return HttpNotFound();
            }
            return View(movimientoPago);
        }

        // GET: MovimientoPagos/Create
        public ActionResult Create()
        {
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion");
            ViewBag.MovimientoCuentaTipoID = new SelectList(db.CargoTipos, "ID", "Descripcion");
            ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion");
            ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre");
            ViewBag.SedeID = new SelectList(db.Sedes, "ID", "Nombre");
            return View();
        }

        // POST: MovimientoPagos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,MovimientoCuentaTipoID,FamiliaID,Fecha,Importe,Saldo,Observaciones,FechaAlta,UsuarioNombre,FormaPagoID,ResponsableID,SedeID,Numero,Anulado")] MovimientoPago movimientoPago)
        {
            if (ModelState.IsValid)
            {
                db.MovimientoCuentas.Add(movimientoPago);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", movimientoPago.FamiliaID);
            ViewBag.MovimientoCuentaTipoID = new SelectList(db.CargoTipos, "ID", "Descripcion", movimientoPago.MovimientoCuentaTipoID);
            ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion", movimientoPago.FormaPagoID);
            ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre", movimientoPago.ResponsableID);
            ViewBag.SedeID = new SelectList(db.Sedes, "ID", "Nombre", movimientoPago.SedeID);
            return View(movimientoPago);
        }

        // GET: MovimientoPagos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimientoPago movimientoPago = await db.MovimientoPagos.FindAsync(id);
            if (movimientoPago == null)
            {
                return HttpNotFound();
            }
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", movimientoPago.FamiliaID);
            ViewBag.MovimientoCuentaTipoID = new SelectList(db.CargoTipos, "ID", "Descripcion", movimientoPago.MovimientoCuentaTipoID);
            ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion", movimientoPago.FormaPagoID);
            ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre", movimientoPago.ResponsableID);
            ViewBag.SedeID = new SelectList(db.Sedes, "ID", "Nombre", movimientoPago.SedeID);
            return View(movimientoPago);
        }

        // POST: MovimientoPagos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,MovimientoCuentaTipoID,FamiliaID,Fecha,Importe,Saldo,Observaciones,FechaAlta,UsuarioNombre,FormaPagoID,ResponsableID,SedeID,Numero,Anulado")] MovimientoPago movimientoPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movimientoPago).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", movimientoPago.FamiliaID);
            ViewBag.MovimientoCuentaTipoID = new SelectList(db.CargoTipos, "ID", "Descripcion", movimientoPago.MovimientoCuentaTipoID);
            ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion", movimientoPago.FormaPagoID);
            ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre", movimientoPago.ResponsableID);
            ViewBag.SedeID = new SelectList(db.Sedes, "ID", "Nombre", movimientoPago.SedeID);
            return View(movimientoPago);
        }

        // GET: MovimientoPagos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimientoPago movimientoPago = await db.MovimientoPagos.FindAsync(id);
            if (movimientoPago == null)
            {
                return HttpNotFound();
            }
            return View(movimientoPago);
        }

        // POST: MovimientoPagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MovimientoPago movimientoPago = await db.MovimientoPagos.FindAsync(id);
            db.MovimientoCuentas.Remove(movimientoPago);
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
