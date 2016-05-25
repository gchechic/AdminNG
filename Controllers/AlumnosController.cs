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
using PagedList;

namespace AdminNG.Controllers
{
    public class AlumnosController : Controller
    {
        private AdminNGContext db = new AdminNGContext();

        // GET: Alumnos
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.ApellidoSortParm = String.IsNullOrEmpty(sortOrder) ? "apellido_desc" : "";
            ViewBag.CursoSortParm = sortOrder == "Curso" ? "curso_desc" : "Curso";
            var alumnos = from s in db.Alumnos
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                alumnos = alumnos.Where(s => s.Apellido.Contains(searchString)
                                       || s.Nombre.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "apellido_desc":
                    alumnos = alumnos.OrderByDescending(s => s.Apellido);
                    break;
                case "Curso":
                    alumnos = alumnos.OrderBy(s => s.InscripcionActiva.Curso.Codigo);
                    break;
                case "curso_desc":
                    alumnos = alumnos.OrderByDescending(s => s.InscripcionActiva.Curso.Codigo);
                    break;
                default:
                    alumnos = alumnos.OrderBy(s => s.Apellido);
                    break;
            }
            //var alumnos = db.Alumnos.Include(a => a.Inscripcion);
            return View(alumnos.ToList());
        }

        // GET: Alumnos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumno alumno = db.Alumnos.Find(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            return View(alumno);
        }

        // GET: Alumnos/Create
        public ActionResult Create()
        {
            PopulateCursosDropDownList();            
            return View();
        }

        // POST: Alumnos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Apellido,Nombre")] Alumno alumno, int? CursoID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    db.Alumnos.Add(alumno);

                    // Si se eligio curso Crear Inscripcion
                    if (CursoID.HasValue)
                    {
                        Curso curso = db.Cursos.Find(CursoID);
                        Inscripcion inscripcion = new Inscripcion() { Alumno = alumno, Curso = curso, FechaAlta = DateTime.Now };
                        db.Inscripciones.Add(inscripcion);
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //ViewBag.ID = new SelectList(db.Inscripciones, "ID", "ID", alumno.ID);
            return View(alumno);
        }

        // GET: Alumnos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumno alumno = db.Alumnos.Find(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            if (alumno.InscripcionActiva!= null)
                PopulateCursosDropDownList(alumno.InscripcionActiva.Curso.ID);
            else
                PopulateCursosDropDownList();
            return View(alumno);
        }

        // POST: Alumnos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, int? cursoID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumno alumnoToUpdate = db.Alumnos.Find(id);

            if (TryUpdateModel(alumnoToUpdate, "",
                 new string[] { "Nombre", "Apellido"}))
            {
                try
                {
                    //cambiar curso
                    Inscripcion inscripcionActual = alumnoToUpdate.InscripcionActiva;
                    if (inscripcionActual != null && cursoID != inscripcionActual.Curso.ID)
                    {
                        inscripcionActual.FechaBaja = DateTime.Now;
                        if (cursoID.HasValue)
                        {
                            db.Inscripciones.Add(new Inscripcion { Alumno = alumnoToUpdate, Curso = db.Cursos.Find(cursoID), FechaAlta = DateTime.Now });
                        }
                    }
                    else if (inscripcionActual == null)
                    {
                        db.Inscripciones.Add(new Inscripcion { Alumno = alumnoToUpdate, Curso = db.Cursos.Find(cursoID), FechaAlta = DateTime.Now });
                    }
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(alumnoToUpdate);
        }

        // GET: Alumnos/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Alumno alumno = db.Alumnos.Find(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            return View(alumno);
        }

        // POST: Alumnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Alumno alumno = db.Alumnos.Find(id);
                db.Alumnos.Remove(alumno);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                return RedirectToAction("Delete", new { id= id, saveChangesError = true});
            }
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

        private void PopulateCursosDropDownList(object selectedCurso= null)
        {
            var cursosQuery = from d in db.Cursos
                                   orderby d.Codigo
                                   select d;
            ViewBag.CursoID = new SelectList(cursosQuery, "ID", "Codigo", selectedCurso);
        } 

    }
}
