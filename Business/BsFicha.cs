using AdminNG.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.Business
{
    public class BSFicha: BSBase
    {
        public BSFicha(AdminNGContext db)
        {
            this.db = db;
        }
        public double Saldo(int FamiliaID, DateTime dtFecha)
        {
            AdminNG.Business.BSMovimientoCuenta bsMovimientoCuenta = new BSMovimientoCuenta(db);
            var saldo = bsMovimientoCuenta.Saldo(FamiliaID);
            BSGtoAdm bsGtoAdm = new BSGtoAdm(db);
            saldo += bsGtoAdm.GtoAdmsImporteCalculado(FamiliaID, dtFecha);
            return saldo;
        }
    }
}