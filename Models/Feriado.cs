using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    public class Feriado
    {
        public int ID { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }       
    }
}