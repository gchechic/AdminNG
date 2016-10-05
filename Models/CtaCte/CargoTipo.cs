using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models.CtaCte
{
    [Table("CargoTipos")]
    public class CargoTipo
    {
        public enum IDS : int
        {
            Cuota,
            GtoAdm,
            Comedor
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [Required]
        public string Descripcion { get; set; } 
    }
}
