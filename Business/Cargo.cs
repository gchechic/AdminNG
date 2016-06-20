using AdminNG.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.Business
{
    public class Cargo
    {
        private AdminNGContext db;
        private Cargo() { }
        public Cargo( AdminNGContext dbContext )
        { db = dbContext; }

        public List<AdminNG.Models.Cargo> CargosPendientes( int FamiliaID)
        {
            var q = from cargo in db.Cargos
                    where 
                    cargo.Inscripcion.Alumno.FamiliaID == FamiliaID &&
                    cargo.Saldo != 0    
                    orderby cargo.Fecha ascending , cargo.ID ascending
                    select cargo ;
            //var q1 = db.Cargos.Where<AdminNG.Models.Cargo>(c => c.Inscripcion.Alumno.FamiliaID == FamiliaID && c.Saldo != 0).OrderBy(c => c.Fecha);
            return q.ToList<AdminNG.Models.Cargo>();
        }
         public double  TotalCargosPendientes( int FamiliaID)
        {
            return CargosPendientes(FamiliaID).Sum(c => c.Importe);
        }
    }
}