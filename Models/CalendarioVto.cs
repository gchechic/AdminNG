using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    public class CalendarioVto
    {
        public enum IDS
        {
            Default =1
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [Required]
        public string Descripcion{ get; set; }

        public virtual ICollection<CalendarioVtoItem> CalendarioVtoItems { get; set; }
     
    }
}