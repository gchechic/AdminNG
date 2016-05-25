using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    [Table("Cursos")]
    public class Curso
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [Required]
        public string Codigo{ get; set; }        
        [Range(0,15) ]
        public int Nivel { get; set; }

        public virtual ICollection<Inscripcion> Inscripciones{ get; set; }
    }
}