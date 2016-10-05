using AdminNG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.DAL
{
    public class DBCurso : DBBase
    {
        public List<Curso> GetCursos()
        {
            var cursos = db.Cursos.ToList();
            return cursos;
        }
        public Curso GetCurso( int id)
        {
            var curso = db.Cursos.Find(id);
            return curso;
        }
        public bool CheckCursoExist(int id)
        {
            var curso = db.Cursos.Find(id);            
            return curso != null ;
        }
    }
}