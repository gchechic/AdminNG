using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    [Table("CargoCodigoValores")]
    public class CargoCodigoValor
    {
        public enum IDS:int
        { Completa = 1,
          SegundoHijo =2,
          TercerHijo = 3,
          CuartoHijo= 4,
          QuintoHijo = 5,
          GtoAdm1 = 101,
          GtoAdm2= 102,
          Comedor = 300, // Codigo de cargo para comedor. Se asocia a los cursos. Puede ser o 302 o 303
          Comedorx2=302, //comedor 2 veces x semana
          Comedorx3 = 303 //comedor 3 veces x semana
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [StringLength(20)]
        public string Descripcion { get; set; }
        public bool EsCuota { get; set; } 
    }
}