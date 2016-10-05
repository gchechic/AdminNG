using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
     [Table("CargoValores")]
    public class CargoValor
    {
        public int ID { get; set; }
        [Index("IX_Curso_Codigo_Fecha", IsUnique = true, Order = 1)]
        public int CursoID { get; set; }
        [Index("IX_Curso_Codigo_Fecha", IsUnique = true, Order = 2)]
        public int CargoCodigoValorID { get; set; }
        [Required]
        [Index("IX_Curso_Codigo_Fecha", IsUnique = true, Order = 3)]
        public DateTime FechaDesde{ get; set; }
        [Required]
        public double Valor{ get; set; }
        [Required]
        public virtual CargoCodigoValor CargoCodigo { get; set; }
        [Required]
        public virtual Curso Curso { get; set; }

    }
}