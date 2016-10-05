using AdminNG.Models;
using AdminNG.Models.CtaCte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.ViewModels
{
    public class VMFicha
    {
        //public int FamiliaID { get; set; }
        public Familia Familia { get; set; }
        public double Saldo{ get; set; }
        public IEnumerable< MovimientoCargo > CargosPendientes { get; set; }       
    }
}