using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.DAL
{
    public abstract class DBBase
    {
        protected AdminNGContext db = new AdminNGContext();
        
    }
}