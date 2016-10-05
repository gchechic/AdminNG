using AdminNG.DAL;
using AdminNG.Models;
using AdminNG.Models.CtaCte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminNG.Business
{
    public class BSComedor : BSBase
    {
        public BSComedor(AdminNGContext db)
        {
            this.db = db;
        }
        public List<CargoComedor> GetCargoComedoresMes(int mes)
        {
            List<CargoComedor> listComedores = new List<CargoComedor>();
            BSInscripcion bsInscripcion = new BSInscripcion(db);
            bsInscripcion.getInscripcionesComedorVigentesMes(mes).ForEach(i => listComedores.Add(GetComedor(i, mes)));//Vigentes al mes anterior

            return listComedores.Where( c=>c.Importe >0).ToList();
        }
        public CargoComedor GetComedor(Inscripcion inscripcionComedor, int mes)
        {
            DateTime dtFechaCalculo = new DateTime(DateTime.Today.Year, mes, 1); // primero del mes 
            int codigoValorID = inscripcionComedor.CargoCodigoValorID;
            double valor = BSCargoValor.GetValor(inscripcionComedor.CursoID, inscripcionComedor.CargoCodigoValorID, dtFechaCalculo);
            CargoComedor comedor = new CargoComedor
            {
                FamiliaID = inscripcionComedor.Alumno.FamiliaID,
                InscripcionID = inscripcionComedor.ID,
                Fecha = dtFechaCalculo,
                Mes = mes,
                CargoCodigoValorID = codigoValorID,
                Importe = valor,
                Saldo = valor,
                FechaAlta = DateTime.Now,
                Usuario = AdminNG.Helpers.Configuracion.UsuarioSistema
            };
            return comedor;
        }
    }
}