using AdminNG.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.Business
{
    public static class CargoValor
    {
        private static AdminNGContext db = new AdminNGContext();
        public static double GetValor( int CursoID, int CargoCodigoValorID,  DateTime Fecha)
        {
            double result =0;
            var query = (from v in db.CargoValores
                         where 
                         v.CursoID == CursoID &&
                         v.CargoCodigoValorID == CargoCodigoValorID &&
                         v.FechaDesde <= Fecha 
                         orderby Fecha descending
                         select v).FirstOrDefault();
            if (query  != null )
                result = query.Valor;
            return result;
        }

    }
}