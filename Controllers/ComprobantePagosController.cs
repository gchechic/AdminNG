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
    public class ComprobantePagosController : Controller
    {
        private AdminNGContext db = new AdminNGContext();

        // GET: ComprobantePagos
        public async Task<ActionResult> Index()
        {
            var comprobantePagos = db.MovimientoPagos.Include(c => c.Familia).Include(c => c.FormaPago);
            return View(await comprobantePagos.ToListAsync());
        }

        // GET: ComprobantePagos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComprobantePago comprobantePago = await db.MovimientoPagos.FindAsync(id);
            if (comprobantePago == null)
            {
                return HttpNotFound();
            }
            return View(comprobantePago);
        }

        // GET: ComprobantePagos/Create
        public ActionResult Create()
        {
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion");
            ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion");
            return View();
        }

        // POST: ComprobantePagos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,FamiliaID,FormaPagoID,Numero,Fecha,Importe,Saldo,Observaciones,Anulado,FechaAlta,UserName")] ComprobantePago comprobantePago)
        {
            if (ModelState.IsValid)
            {
                db.MovimientoPagos.Add(comprobantePago);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", comprobantePago.FamiliaID);
            ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion", comprobantePago.FormaPagoID);
            return View(comprobantePago);
        }

        // GET: ComprobantePagos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComprobantePago comprobantePago = await db.MovimientoPagos.FindAsync(id);
            if (comprobantePago == null)
            {
                return HttpNotFound();
            }
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", comprobantePago.FamiliaID);
            ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion", comprobantePago.FormaPagoID);
            return View(comprobantePago);
        }

        // POST: ComprobantePagos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,FamiliaID,FormaPagoID,Numero,Fecha,Importe,Saldo,Observaciones,Anulado,FechaAlta,UserName")] ComprobantePago comprobantePago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comprobantePago).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", comprobantePago.FamiliaID);
            ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion", comprobantePago.FormaPagoID);
            return View(comprobantePago);
        }

        // GET: ComprobantePagos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComprobantePago comprobantePago = await db.MovimientoPagos.FindAsync(id);
            if (comprobantePago == null)
            {
                return HttpNotFound();
            }
            return View(comprobantePago);
        }

        // POST: ComprobantePagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ComprobantePago comprobantePago = await db.MovimientoPagos.FindAsync(id);
            db.MovimientoPagos.Remove(comprobantePago);
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
