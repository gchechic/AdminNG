using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    [Table("Alumnos")]
    public class Alumno
    {
        public int ID { get; set; }
        public int FamiliaID { get; set; } 
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Nombre { get; set; }
        public bool Comedor { get; set; }
        public virtual Familia Familia { get; set; }
        public virtual ICollection<Inscripcion> Inscripciones { get; set; }
        public  virtual  Inscripcion InscripcionActiva { get
            {
            return Inscripciones.FirstOrDefault ( i => i.FechaAlta <= DateTime.Now && (i.FechaBaja == null || i.FechaBaja >= DateTime.Now ));
            }
        }
    }
}