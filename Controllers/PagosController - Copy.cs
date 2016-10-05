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
    public class PagosController : Controller
    {
        private AdminNGContext db = new AdminNGContext();
        private AdminNG.Business.Pago BSPago;

        public PagosController()
        {
            BSPago = new Business.Pago();

        }
        // GET: Pagos
        public async Task<ActionResult> Index()
        {
            var pagoes = db.Pagos.Include(p => p.Familia).Include(p => p.FormaPago).Include(p => p.Responsable).Include(p => p.Sede);
            return View(await pagoes.ToListAsync());
        }

        // GET: Pagos/Details/5
        public async Task<ActionResult> Details(int? id)
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

        // GET: Pagos/Create
        public ActionResult Create()
        {
            Pago pago = new Pago();
            pago.Fecha = DateTime.Today;
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion");
            ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion");
            ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre");
            ViewBag.SedeID = new SelectList(db.Sedes, "ID", "Nombre");
            return View();
        }

        // POST: Pagos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,FamiliaID,FormaPagoID,ResponsableID,SedeID,ComprobanteNumero,Fecha,Importe,Observaciones")] Pago pago)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (pago.FormaPagoID == (int)FormaPago.IDS.Transferencia)
                    {
                        db.PagoTransferencias.Add((PagoTransferencia)pago);
                    }
                    else
                        db.Pagos.Add(pago);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", pago.FamiliaID);
                ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion", pago.FormaPagoID);
                ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre", pago.ResponsableID);
                ViewBag.SedeID = new SelectList(db.Sedes, "ID", "Nombre", pago.SedeID);
            }
            catch (DataException /* dex */)
            {            
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("",  Resources.UnableToSaveChanges);
            }
            return View(pago);
        }

        // GET: Pagos/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
                
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", pago.FamiliaID);
            ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion", pago.FormaPagoID);
            ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre", pago.ResponsableID);
            ViewBag.SedeID = new SelectList(db.Sedes, "ID", "Nombre", pago.SedeID);
            switch (pago.FormaPagoID)
            {
                case (int)FormaPago.IDS.Contado:
                    return RedirectToAction("Edit", "PagoContados", new { id = id });
                    
                case (int)FormaPago.IDS.Transferencia:
                    return RedirectToAction("Edit", "PagoTransferencias", new { id = id });
                    
                case (int)FormaPago.IDS.Deposito:
                    return RedirectToAction("Edit", "PagoDepositos", new { id = id });
                    

                default:
                    return View(pago);
            }
        }

        // POST: Pagos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,FamiliaID,FormaPagoID,ResponsableID,SedeID,ComprobanteNumero,Fecha,Importe,Observaciones,Anulado,FechaAlta,UsuarioNombre")] Pago pago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pago).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", pago.FamiliaID);
            ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion", pago.FormaPagoID);
            ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre", pago.ResponsableID);
            ViewBag.SedeID = new SelectList(db.Sedes, "ID", "Nombre", pago.SedeID);
            return View(pago);
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

        public ActionResult CreatePagoContado(int FamiliaID)
        {            
            //ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion");
            //ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre");
            PagoContado pagoContado = new PagoContado();
            pagoContado.Fecha = DateTime.Now;
            pagoContado.Familia = db.Familia.Find(FamiliaID);
            pagoContado.FamiliaID = FamiliaID;
            var saldo = new AdminNG.Business.MovimientoCuenta().TotalCargosPendientes(FamiliaID);
            if (saldo>0)
                pagoContado.ImporteEfectivo = new AdminNG.Business.MovimientoCuenta().TotalCargosPendientes(FamiliaID);
            pagoContado.SedeID = (int)Session["SedeID"]; //sacarlo del usuario
            pagoContado.ComprobanteNumero = BSPago.PagoContadoComprobanteNumeroUltimo(pagoContado.SedeID)+1;
            return View(pagoContado);
        }

        // POST: Recibos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePagoContado([Bind(Include = "FamiliaID,ComprobanteNumero,Fecha,Observaciones,ImporteEfectivo,ImporteCheques,DetalleCheques")] PagoContado pagoContado)            
        {
            try
            {
                AdminNG.Business.Pago BSPago = new Business.Pago();

                pagoContado.SedeID = (int)Session["SedeID"]; //sacarlo del usuario
                if (await BSPago.CrearPagoContado(pagoContado, ModelState))
                {
                    return RedirectToAction("Index");
                }

                ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", pagoContado.FamiliaID);
                ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion", pagoContado.FormaPagoID);
                ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre", pagoContado.ResponsableID);
            }
            catch (DataException )
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //ViewBag.ID = new SelectList(db.Inscripciones, "ID", "ID", alumno.ID);
            return View(pagoContado);
        }

        //public ActionResult CreatePagoTransferencia()
        //{
        //    ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion");
        //    ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre");
        //    PagoContado pagoContado = new PagoContado();
        //    pagoContado.Fecha = DateTime.Now;
        //    pagoContado.Familia = db.Familia.Find(FamiliaID);
        //    pagoContado.FamiliaID = FamiliaID;

        //    pagoContado.ImporteEfectivo = new AdminNG.Business.MovimientoCuenta().TotalCargosPendientes(FamiliaID);
        //    pagoContado.SedeID = (int)Session["SedeID"]; //sacarlo del usuario
        //    pagoContado.ComprobanteNumero = BSPago.PagoContadoComprobanteNumeroUltimo(pagoContado.SedeID) + 1;
        //    return View(pagoContado);
        //}
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
