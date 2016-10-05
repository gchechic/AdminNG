using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminNG.Models
{    
    public class MovimientoPago: MovimientoCuenta
    {
        public int PagoID;

        public Pago Pago;

    }
}