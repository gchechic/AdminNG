using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    public class Tope
    {
        public int ID { get; set; }
        public int Mes { get; set; }
        public int ResponsableID { get; set; }
        public double Importe { get; set; }
        public double Acumulado { get; set; }
        public virtual Responsable Responsable { get; set; }
    }
}