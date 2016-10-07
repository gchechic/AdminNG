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
using AdminNG.Helpers;
using System.Data.Entity.Infrastructure;
using AdminNG.Business;

namespace AdminNG.Controllers
{
    public class PagoContadosController : Controller
    {
        private AdminNGContext db = new AdminNGContext();
        private AdminNG.Business.BSPago BSPago;
        public PagoContadosController()
        {
            BSPago = new Business.BSPago(db);
        }


        public ActionResult CreateRecibo(int FamiliaID)
        {
            //ViewBag.FormaPagoID = new SelectList(db.FormaPagos, "ID", "Descripcion");
            //ViewBag.ResponsableID = new SelectList(db.Responsables, "ID", "Nombre");
            PagoContado pagoContado = new PagoContado();
            pagoContado.FamiliaID = FamiliaID;
            pagoContado.SedeID = (int)Session["SedeID"];

            BSPago.InicializarPagoContado(pagoContado);
            ViewBag.Saldo = new BSFicha(db).Saldo(pagoContado.FamiliaID, pagoContado.Fecha);
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
                AdminNG.Business.BSPago BSPago = new Business.BSPago(db);

                pagoContado.Usuario = System.Web.HttpContext.Current.User.Identity.Name;
                pagoContado.SedeID = (int)Session["SedeID"];

                if (await BSPago.CrearPagoContado(pagoContado, ModelState))
                {
                    return RedirectToAction("Index", "Pagos");
                }
                //db.Entry(pagoContado).Reference( p => p.Familia ).Load();
                pagoContado.Familia = db.Familia.Find(pagoContado.FamiliaID);

            }
            catch (RetryLimitExceededException ex)
            {
                LogHelper.LogExcepcion(ex);
                ModelState.AddModelError("", AdminNG.Properties.Resources.UnableToSaveChanges);
            }
            //ViewBag.ID = new SelectList(db.Inscripciones, "ID", "ID", alumno.ID);
            return View(pagoContado);
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
            //pagoContado.Importe *= -1; 
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
