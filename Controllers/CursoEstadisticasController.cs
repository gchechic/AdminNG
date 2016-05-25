using AdminNG.DAL;
using AdminNG.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminNG.Controllers
{
    public class CursoEstadisticasController : Controller
    {
        private AdminNGContext db = new AdminNGContext();

        [HttpGet, ActionName("AlumnosGroup")]
        public ActionResult AlumnosGroup()
        {

            var cursoCounts = from i in db.Inscripciones
                              group i by i.Curso into g
                              select new CursoAlumnosGroup() { Curso = g.Key, AlumnoCount = g.Count() };

            return View(cursoCounts.OrderBy(g => g.Curso.Codigo).ToList());
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
