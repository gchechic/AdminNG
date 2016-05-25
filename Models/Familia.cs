using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    public class Familia
    {
        public int ID { get; set; }
        public string Descripcion{ get; set; }

        public virtual ICollection<Familiar> Familiares { get; set; }
        public virtual ICollection<Alumno> Alumnos{ get; set; }
    }
}