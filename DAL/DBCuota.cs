using AdminNG.Models.CtaCte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.DAL
{
    public class DBCuota: DBBase 
    {
        public DBCuota(AdminNGContext db)
        { 
            this.db = db;       
        }
        public List<AdminNG.Models.CtaCte.CargoCuota> CuotasConSaldo()
        {
            var q = from cuota in db.CargoCuotas
                    where
                    cuota.Saldo != 0
                    orderby cuota.Fecha ascending, cuota.ID ascending
                    select cuota;
            
            return q.ToList<AdminNG.Models.CtaCte.CargoCuota>();
        }
        public IEnumerable<CargoCuota> CuotasImpagas()
        {
            return db.CargoCuotas.Where(c => c.Saldo != 0);
        }

    }
}