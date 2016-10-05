using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.ViewModels
{
    public class VMAlumno
    {
        public int ID { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string CursoCodigo { get; set; }
        public int FamiliaID{ get; set; }
    }
}