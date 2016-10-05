using AdminNG.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{
    public class Repositorio
    {
        private AdminNGContext db = new AdminNGContext();

        public List<Responsable> GetResonsables()
        {
            return db.Responsables.ToList();
        }
        public List<Responsable> GetResonsablesNoR()
        {
            return db.Responsables.Where(r => r.Grade != Grade.R).ToList();
        }

    }
}