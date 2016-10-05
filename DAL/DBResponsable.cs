
using AdminNG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.DAL
{
    public class DBResponsable: DBBase 
    {
        
        public List<Responsable> GetResonsables()
        {
            return db.Responsables.ToList();
        }
        public Responsable GetResonsable( int id)
        {
            return db.Responsables.Find(id);
        }

        public List<Responsable> GetResonsablesNoR()
        {
            return db.Responsables.Where(r => r.Grade != Grade.R).ToList();
        }

    }
}