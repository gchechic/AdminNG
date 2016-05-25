using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminNG.DAL;
using AdminNG.ViewModels;

namespace AdminNG.Controllers
{
    public class CursosAlumnosController : Controller
    {
        private AdminNGContext db = new AdminNGContext();

        public ActionResult Index()
        {

            var cursoCounts = from i in db.Inscripciones
                              group i by  i.Curso into g
                              select new CursoAlumnosGroup() { Curso = g.Key, AlumnoCount = g.Count() }; 

            return View(cursoCounts.OrderBy(g=>g.Curso.Codigo).ToList());
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}