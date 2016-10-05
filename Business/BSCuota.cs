using AdminNG.DAL;
using AdminNG.Models;
using AdminNG.Models.CtaCte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.Business
{

    public class BSCuota:BSBase
    {
        public BSCuota(AdminNGContext db)
        { 
            this.db = db;
           
        }
        public IEnumerable<CargoCuota> GetCuotasImpagas()
        {
            var cuotasImpagas = db.CargoCuotas.Where(
                     m => m.Importe != 0 && ((m.Importe - m.Saldo) / m.Importe < AdminNG.Helpers.Configuracion.PorcCuotaImpaga)
                                );
            return cuotasImpagas;
        }
        public IEnumerable<CargoCuota> GetCuotasImpagas(int FamiliaID)
        {

            var cuotasImpagas = db.CargoCuotas.Where(
                     m => m.FamiliaID == FamiliaID &&
                         m.Importe != 0 && ((m.Importe - m.Saldo) / m.Importe < AdminNG.Helpers.Configuracion.PorcCuotaImpaga)
                                );
            return cuotasImpagas;
        }
        public List<CargoCuota> GetCargoCuotasMes(int mes)
        {            
            List<CargoCuota> listCuotas = new List<CargoCuota>();
            BSInscripcion bsInscripcion = new BSInscripcion(db);
            bsInscripcion.getInscripcionesCursoVigentesMes(mes).ForEach(i => listCuotas.Add(GetCuota(i, mes)));
          
            return listCuotas;
        }
        public CargoCuota GetCuota(Inscripcion inscripcion, int mes)
        {
            DateTime dtFechaCalculo = new DateTime(DateTime.Today.Year, mes, 1); // primero del mes 
            int codigoValorID = inscripcion.CargoCodigoValorID;
            double valor = BSCargoValor.GetValor(inscripcion.CursoID, inscripcion.CargoCodigoValorID, dtFechaCalculo);
            CargoCuota cuota = new CargoCuota
            {
                FamiliaID = inscripcion.Alumno.FamiliaID,
                InscripcionID = inscripcion.ID,
                Fecha = dtFechaCalculo,
                Mes = mes,
                CargoCodigoValorID = codigoValorID,
                Importe = valor,
                Saldo = valor,
                FechaAlta = DateTime.Now,
                Usuario = AdminNG.Helpers.Configuracion.UsuarioSistema
            };
            return cuota;
        }
    }
}