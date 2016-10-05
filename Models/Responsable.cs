using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    public enum Grade
    {
        A, B, C, R
    }
    public class Responsable
    {        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        public Grade? Grade { get; set; }
    }
}