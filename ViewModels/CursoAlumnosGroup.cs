using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminNG.Models;

namespace AdminNG.ViewModels
{
    public class CursoAlumnosGroup
    {
        public Curso Curso { get; set; }
        public int AlumnoCount { get; set; }
    }
}