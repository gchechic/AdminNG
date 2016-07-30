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
            bsInscripcion.getInscripcionesComedorVigentesMes(mes-1).ForEach(i => listComedores.Add(GetComedor(i, mes)));//Vigentes al mes anterior

            return listComedores;
        }
        public CargoComedor GetComedor(Inscripcion inscripcion, int mes)
        {
            DateTime dtFechaCalculo = new DateTime(DateTime.Today.Year, mes, 1); // primero del mes 
            int codigoValorID = inscripcion.CargoCodigoValorID;
            double valor = BSCargoValor.GetValor(inscripcion.CursoID, inscripcion.CargoCodigoValorID, dtFechaCalculo);
            CargoComedor comedor = new CargoComedor
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
            return comedor;
        }
    }
}