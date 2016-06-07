using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AdminNG.Models
{
    [Table("Familiares")]
    public class Familiar
    {
        public int ID { get; set; }
        public int FamiliaID { get; set; }
//        [Required]
        public string Apellido { get; set; }
        public string Nombre { get; set; }        
        [EmailAddress]        
        public string Email { get; set; }
        [Required]
        public int DNI{ get; set; }

        
        public virtual Familia Familia { get; set; }
        [Required]
        public virtual FamiliaRol FamiliaRol { get; set; }
    }
}