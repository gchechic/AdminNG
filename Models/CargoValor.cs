using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    public class CargoValor
    {
        public int ID { get; set; }
        public int CuotaCodigoID { get; set; }
        public int CursoID { get; set; }
        [Required]        
        public DateTime FechaDesde{ get; set; }
        [Required]
        public double Valor{ get; set; }
        [Required]
        public virtual CargoCodigo CuotaCodigo { get; set; }
        [Required]
        public virtual Curso Curso { get; set; }

    }
}