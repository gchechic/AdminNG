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
        public enum IDS: int
        { Comedor =100}
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [Required]

        [Index("IX_Codigo", IsUnique = true)]
        [StringLength(10)]
        public string Codigo{ get; set; }        
        [Range(0,15) ]
        public int Nivel { get; set; }
        
    }
}