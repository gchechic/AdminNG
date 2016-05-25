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
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Nombre { get; set; }
        
        public virtual Familia Familia { get; set; }
        public virtual ICollection<Inscripcion> Inscripciones { get; set; }
        public  virtual  Inscripcion InscripcionActiva { get
            {
                return Inscripciones.ToList().FirstOrDefault(i => i.IsActiva);
            }
        }
    }
}