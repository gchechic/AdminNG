using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.DAL
{
    public class DBMovimientoCargo: DBBase 
    {
        public DBMovimientoCargo(AdminNGContext db )
        { this.db = db; }
        public List<AdminNG.Models.CtaCte.MovimientoCargo> CargosPendientes(int FamiliaID)
        {
            var q = from cargo in db.MovimientoCargos
                    where
                    cargo.FamiliaID == FamiliaID &&
                    cargo.Saldo != 0
                    orderby cargo.Fecha ascending, cargo.ID ascending
                    select cargo;
            //var q1 = db.Cargos.Where<AdminNG.Models.Cargo>(c => c.Inscripcion.Alumno.FamiliaID == FamiliaID && c.Saldo != 0).OrderBy(c => c.Fecha);
            return q.ToList<AdminNG.Models.CtaCte.MovimientoCargo>();
        }

    }
}