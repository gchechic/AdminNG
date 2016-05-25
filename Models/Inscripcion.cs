using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    [Table("Inscripciones")]
    public class Inscripcion
    {
        public int ID { get; set; }
        [Required]
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        [Required]
        public virtual Curso Curso { get; set; }
        [Required]
        public virtual Alumno Alumno { get; set; }
        public virtual CuotaCodigo CuotaCodigo { get; set; }
        public bool IsActiva
        {
            get { return this.FechaAlta <= DateTime.Now && (this.FechaBaja == null || this.FechaBaja >= DateTime.Now); } 
        }
    }
}