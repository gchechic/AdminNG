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
    public class RecibosController : Controller
    {
        private AdminNGContext db = new AdminNGContext();
        private AdminNG.Business.MovimientoCuenta BSCargo;

        public RecibosController()
        {
            BSCargo = new Business.MovimientoCuenta(db);

        }
        // GET: Recibos
        public async Task<ActionResult> Index()
        {
            var comprobantePagos = db.Recibos.Include(r => r.Familia).Include(r => r.FormaPago).Include(r => r.Responsable);
            return View(await comprobantePagos.ToListAsync());
        }

        // GET: Recibos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoContado recibo = await db.Recibos.FindAsync(id);
            if (recibo == null)
            {
                return HttpNotFound();
            }
            return View(recibo);
        }

        // GET: Recibos/Create
        public ActionResult Create( int FamiliaID)
        {
            //ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion");
            ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion");
            ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre");
            PagoContado recibo = new PagoContado();
            recibo.Fecha = DateTime.Now;
            recibo.Familia = db.Familia.Find(FamiliaID);
            recibo.FamiliaID = FamiliaID;
            recibo.ImporteEfectivo = BSCargo.TotalCargosPendientes(FamiliaID);
            recibo.SedeID = (int)Session["SedeID"]; //sacarlo del usuario
            recibo.ComprobanteNumero = ProximoNro(recibo.SedeID);
            return View(recibo);
        }

        // POST: Recibos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FamiliaID,Numero,Fecha,Observaciones,ImporteEfectivo,ImporteCheques,DetalleCheques")] PagoContado recibo)
        {
            try
            {
                bool bValido = true;
                //validar
                if (recibo.Fecha > DateTime.Today)
                {
                    ModelState.AddModelError("Fecha", "La fecha no puede ser mayor a la actual");
                    bValido = false;
                }
                
                if (bValido)
                { 
                    //Datos Pago
                    recibo.FormaPagoID = (int)FormaPago.IDS.Contado;                    
                    recibo.Importe = recibo.ImporteEfectivo + recibo.ImporteCheques;
                    recibo.Saldo = recibo.Importe;
                    recibo.ResponsableID = ResponsableReciboID();

                    
                    recibo.MovimientoCuentaTipoID = (int)MovimientoCuentaTipo.IDS.Pago;
                    
                    
                    ///TODO poner cuando este habilitado loggin                    
                    recibo.UsuarioNombre = "xAFxxx";// User.Identity.Name;
                    recibo.FechaAlta = DateTime.Now;
                    recibo.SedeID = (int)Session["SedeID"]; //sacarlo del usuario

                    ModelState.Remove("UsuarioNombre");   //????       
                    
                }
                
                if (ModelState.IsValid)
                {                        
                    db.Recibos.Add(recibo);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", recibo.FamiliaID);
                ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion", recibo.FormaPagoID);
                ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre", recibo.ResponsableID);                
            }
            catch (DataException  dex )
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //ViewBag.ID = new SelectList(db.Inscripciones, "ID", "ID", alumno.ID);
            return View(recibo);
        }

        // GET: Recibos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoContado recibo = await db.Recibos.FindAsync(id);
            if (recibo == null)
            {
                return HttpNotFound();
            }
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", recibo.FamiliaID);
            ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion", recibo.FormaPagoID);
            ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre", recibo.ResponsableID);
            return View(recibo);
        }

        // POST: Recibos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,FamiliaID,FormaPagoID,ResponsableID,Numero,Fecha,Importe,Saldo,Observaciones,Anulado,FechaAlta,UserName,ImporteEfectivo,ImporteCheques,DetalleCheques")] PagoContado recibo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recibo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", recibo.FamiliaID);
            ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion", recibo.FormaPagoID);
            ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre", recibo.ResponsableID);
            return View(recibo);
        }

        // GET: Recibos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoContado recibo = await db.Recibos.FindAsync(id);
            if (recibo == null)
            {
                return HttpNotFound();
            }
            return View(recibo);
        }

        // POST: Recibos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PagoContado recibo = await db.Recibos.FindAsync(id);
            db.Recibos.Remove(recibo);
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

        //business
        private int ResponsableReciboID()
        {
            var q = from r in db.Responsables
                    where r.Grade.Value == Grade.R
                    select r;
            return q.First<Responsable>().ID; 
        }
        private int ProximoNro(int intSedeID)
        {
            ///TODO: USAR SP            
            //int? n = db.Recibos.DefaultIfEmpty().Max(r => r.Numero);
            //return n.Value + 1;
            var x = (from y in db.Recibos
                    select y.ComprobanteNumero).DefaultIfEmpty().Max();
            return x+1;
        }
    }
}
