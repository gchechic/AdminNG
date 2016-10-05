using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    public class Familia
    {
        public int ID { get; set; }
        [StringLength(100)]
        [Index("IX_Descripcion",IsUnique=true)]
        public string Descripcion { get; set; }


        public virtual ICollection<Familiar> Familiares { get; set; }
        public virtual ICollection<Alumno> Alumnos{ get; set; }
    }
}