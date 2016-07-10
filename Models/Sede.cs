using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    public class Sede
    {
        public enum IDS : int
        {
            Bulnes = 1,
            Ugarteche = 2
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre{ get; set; }
        public int ResponsableID { get; set; }
    }
}