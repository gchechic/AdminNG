using AdminNG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.DAL
{
    public class DBInscripcion:DBBase 
    {
        public List<Inscripcion> GetInscripciones()
        {
            var inscripciones = db.Inscripciones.ToList();
            return inscripciones;
        }
        public Inscripcion GetInscripcion( int ID)
        {
            var inscripcion = db.Inscripciones.Find(ID);
            return inscripcion;
        }
    }
}