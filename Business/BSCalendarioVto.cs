using AdminNG.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.Business
{
    public static class BSCalendarioVto
    {
        private static AdminNGContext db = new AdminNGContext();
        public static Models.CalendarioVtoItem getCalendarioVtoItem(int Mes)
        {
            return getCalendarioVtoItem((int)Models.CalendarioVto.IDS.Default, Mes);
        }
        public static Models.CalendarioVtoItem getCalendarioVtoItem( int InscripcionID, int Mes)
        {
            int? CalendarioVtoID = new DBInscripcion().GetInscripcion(InscripcionID).CalendarioVtoID;
            if (!CalendarioVtoID.HasValue)
                CalendarioVtoID = (int)Models.CalendarioVto.IDS.Default;
            return db.CalendarioVtoItems.Where(i => i.CalendarioVtoID == CalendarioVtoID && i.Mes == Mes).FirstOrDefault();
        }
    }
}