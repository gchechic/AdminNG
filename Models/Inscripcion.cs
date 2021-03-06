﻿using System;
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
        public int CursoID { get; set; }
        public int AlumnoID { get; set; }
        public int CargoCodigoValorID{ get; set; }
        public int? CalendarioVtoID { get; set; }
        [Required]
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        
        public virtual Curso Curso { get; set; }
        
        public virtual Alumno Alumno { get; set; }
        public virtual CargoCodigoValor CargoCodigoValor { get; set; }
        public virtual CalendarioVto CalendarioVto { get; set; }    

    }
}