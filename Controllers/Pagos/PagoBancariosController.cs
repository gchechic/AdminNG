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
    public class PagoBancariosController : Controller
    {
        private AdminNGContext db = new AdminNGContext();
        private DBResponsable _repositorio = new DBResponsable();
        private Business.BSPago BSPago;

        public PagoBancariosController()
        {
            BSPago = new Business.BSPago(db);

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
            //pagoBancario.Importe *= -1; 
            return View(pagoBancario);
        }

        // GET: PagoBancarios/Create
        public ActionResult Create( int FamiliaID)
        {
            var responsables = _repositorio.GetResonsablesNoR();
            responsables.Insert(0, null );
                      
            PagoBancario pagoBancario = new PagoBancario();
            pagoBancario.FamiliaID = FamiliaID;
            pagoBancario.SedeID = (int)Session["SedeID"]; //sacarlo del usuario
                        
            BSPago.InicializarPagoBancario(pagoBancario);
            ViewBag.Saldo = new BSFicha(db).Saldo(pagoBancario.FamiliaID, pagoBancario.Fecha);
            ViewBag.ResponsableID = new SelectList(responsables, "ID", "Nombre", pagoBancario.ResponsableID );            
            return View(pagoBancario);   

        }

        // POST: PagoBancarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FamiliaID,ResponsableID,ComprobanteNumero,EsDeposito,Fecha,Importe,DetalleBancario,Observaciones,FechaBanco")] PagoBancario pagoBancario)
        {
            try
            {
                AdminNG.Business.BSPago BSPago = new Business.BSPago(db);
                var responsables = _repositorio.GetResonsablesNoR();
                pagoBancario.SedeID = (int)Session["SedeID"]; 
                pagoBancario.Usuario = System.Web.HttpContext.Current.User.Identity.Name;

                if (await BSPago.CrearPagoBancario(pagoBancario, ModelState))
                {
                    return RedirectToAction("Index", "Pagos");
                }

                pagoBancario.Familia = db.Familia.Find(pagoBancario.FamiliaID); 
                ViewBag.ResponsableID = new SelectList(responsables, "ID", "Nombre", pagoBancario.ResponsableID);
                //ViewBag.FamiliaID = new SelectList(db.Familia, "ID", "Descripcion", pagoBancario.FamiliaID);
            }
            catch (RetryLimitExceededException ex)
            {
                LogHelper.LogExcepcion(ex);
                ModelState.AddModelError("", AdminNG.Properties.Resources.UnableToSaveChanges);
            }
                      
            return View(pagoBancario);
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
