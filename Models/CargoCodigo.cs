using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    [Table("CargoCodigos")]
    public class CargoCodigo
    {
        public enum CargoCodigoIDs
        { Completa = 1,
          SegundoHijo =2,
          TercerHijo = 3,
          CuartoHijo= 4,
          QuintoHijo = 5,
          Mora1 = 101,
          Mora2= 102,
          Comedor=300
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public bool EsCuota { get; set; } 
    }
}