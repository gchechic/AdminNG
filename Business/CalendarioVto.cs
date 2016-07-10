using AdminNG.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.Business
{
    public static class CalendarioVto
    {
        private static AdminNGContext db = new AdminNGContext();
        public static Models.CalendarioVtoItem getCalentarioVtoItem(int Mes)
        {
            return getCalentarioVtoItem((int)Models.CalendarioVto.IDS.Default, Mes);
        }
        public static Models.CalendarioVtoItem getCalentarioVtoItem( int? CalendarioVtoID, int Mes)
        {
            if (!CalendarioVtoID.HasValue)
                CalendarioVtoID = (int)Models.CalendarioVto.IDS.Default;
            return db.CalendarioVtoItems.Where(i => i.CalendarioVtoID == CalendarioVtoID && i.Mes == Mes).FirstOrDefault();
        }
    }
}