using AdminNG.DAL;
using AdminNG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.Business
{
    public class BSInscripcion : BSBase
    {
        public BSInscripcion(AdminNGContext db)
        {
            this.db = db;
        }
        public List<Inscripcion> getInscripcionesCursoVigentesMes( int intMes)
        {
            DateTime dtFechaCalculo = new DateTime( DateTime.Now.Year,intMes,1);
            List<Inscripcion> inscripciones = new List<Inscripcion>();
            var l = db.Inscripciones.Where(i =>i.CursoID != (int)Curso.IDS.Comedor && i.FechaAlta <= dtFechaCalculo && (i.FechaBaja == null || i.FechaBaja >= dtFechaCalculo));
            return l.ToList();
        }
        public List<Inscripcion> getInscripcionesComedorVigentesMes(int intMes)
        {
            DateTime dtFechaCalculo = new DateTime(DateTime.Now.Year, intMes, 1);
            List<Inscripcion> inscripciones = new List<Inscripcion>();
            var l = db.Inscripciones.Where(i => i.CursoID == (int)Curso.IDS.Comedor && i.FechaAlta <= dtFechaCalculo && (i.FechaBaja == null || i.FechaBaja >= dtFechaCalculo));
            return l.ToList();
        }
    }
}