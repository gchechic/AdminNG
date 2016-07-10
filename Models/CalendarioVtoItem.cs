using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    [Table("CalendarioVtoItems")]
    public class CalendarioVtoItem
    {            
        public int ID { get; set; }
        [Index("IX_Calendario_Mes", IsUnique = true, Order = 1)]
        public int CalendarioVtoID { get; set; }
        [Required]
        [Range(1, 12)]
        [Index("IX_Calendario_Mes", IsUnique = true, Order = 2)]
        public int Mes { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PrimerVto { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SegundoVto { get; set; }

     
    }
}