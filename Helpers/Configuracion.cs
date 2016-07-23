using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AdminNG.Helpers
{
    public static class Configuracion
    {
        private static string usuarioSistema = "Sistema";

        public static Single PorcCuotaImpaga
        {
            get
            {
                return Single.Parse(WebConfigurationManager.AppSettings["PorcCuotaImpaga"]);
            }
        }
        public static string UsuarioSistema
        {
            get
            {
                return usuarioSistema;
            }
        }

    }
}