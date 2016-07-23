using AdminNG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.DAL
{
    public class DBAlumno:DBBase
    {
        public List<Alumno> GetAlumnos()
        {
            var alumnos = db.Alumnos.ToList();
            return alumnos;
        }
        public Alumno GetAlumno(int id)
        {
            var alumno = db.Alumnos.Find(id);
            return alumno;
        }
        public bool CheckAlumnoExist(int id)
        {
            var alumno = db.Alumnos.Find(id);
            return alumno != null;
        }
    }
}