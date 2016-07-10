using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminNG.Models;
using System.Data;
using AdminNG.Models.CtaCte;
using AdminNG.Models.Caja;
namespace AdminNG.DAL
{
    public class AdminNGInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<AdminNGContext>
    {
        protected override void Seed(AdminNGContext context)
        {
            try
            {

                var responsables = new List<Responsable>
            {
                new Responsable{ ID = 1, Nombre ="Responsable 1", Grade= Grade.A},
                new Responsable{ ID = 2, Nombre ="Responsable 2", Grade= Grade.C},
                new Responsable{ ID = 3, Nombre ="Responsable 3", Grade= Grade.C},
                new Responsable{ ID = 10, Nombre ="Bulnes", Grade= Grade.R},
                new Responsable{ ID = 11, Nombre ="Ugarteche",  Grade= Grade.R}
            };

                responsables.ForEach(s => context.Responsables.Add(s));
                context.SaveChanges();
                var sedes = new List<Sede>
            {
                new Sede{ ID = (int)Sede.IDS.Bulnes, Nombre ="Bulnes", ResponsableID=10},
                new Sede{ ID = (int)Sede.IDS.Ugarteche, Nombre ="Ugarteche", ResponsableID=11}
            };

                sedes.ForEach(s => context.Sedes.Add(s));
                context.SaveChanges();

                var formapagos = new List<FormaPago>
            {
                new FormaPago{ID=(int)FormaPago.IDS.Contado, Descripcion="Contado" },
                new FormaPago{ID=(int)FormaPago.IDS.Bancario, Descripcion="Bancario" }
            };
                formapagos.ForEach(s => context.FormaPagos.Add(s));
                context.SaveChanges();

               
                var movimientoCuentaTipos = new List<MovimientoCuentaTipo>
            {
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.Cuota, Descripcion="Cuota" },
                 new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.Mora, Descripcion="Mora" },
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.Comedor, Descripcion="Comedor" },
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.Pago, Descripcion="Pago" },
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.Matricula, Descripcion="Matricula" },
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.Bonificacion, Descripcion="Bonificacion" },
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.Redondeo, Descripcion="Redondeo" }
            };
                movimientoCuentaTipos.ForEach(s => context.MovimientoCuentaTipos.Add(s));
                context.SaveChanges();




                var movimientoCajaTipos = new List<MovimientoCajaTipo>
            {
                new MovimientoCajaTipo{ID=(int)MovimientoCajaTipo.IDS.Gasto, Descripcion="Gasto", DebeHaber= DebeHaber.Debe },
                new MovimientoCajaTipo{ID=(int)MovimientoCajaTipo.IDS.Sueldo, Descripcion="Sueldo" },
                new MovimientoCajaTipo{ID=(int)MovimientoCajaTipo.IDS.Extraccion, Descripcion="Extraccion" },
                new MovimientoCajaTipo{ID=(int)MovimientoCajaTipo.IDS.Deposito, Descripcion="Deposito" },
                new MovimientoCajaTipo{ID=(int)MovimientoCajaTipo.IDS.SaldoInicial, Descripcion="Saldo Inicial" },
                new MovimientoCajaTipo{ID=(int)MovimientoCajaTipo.IDS.Redondeo, Descripcion="Redondeo" }
            };
                movimientoCajaTipos.ForEach(s => context.MovimientoCajaTipos.Add(s));
                context.SaveChanges();

                var familiaroles = new List<FamiliaRol>
            {
                new FamiliaRol{ID=1, Descripcion="Padre" },
                new FamiliaRol{ID=2, Descripcion="Madre" }                
            };
                familiaroles.ForEach(s => context.FamiliaRoles.Add(s));
                context.SaveChanges();

                var CargoCodigos = new List<CargoCodigoValor>
            {
             new CargoCodigoValor{ ID= (int)CargoCodigoValor.CargoCodigoValorIDs.Completa, Descripcion = "Completa", EsCuota=true},
             new CargoCodigoValor{ ID= (int)CargoCodigoValor.CargoCodigoValorIDs.SegundoHijo, Descripcion="Primer Hermano", EsCuota=true},
             new CargoCodigoValor{ ID= (int)CargoCodigoValor.CargoCodigoValorIDs.TercerHijo, Descripcion="Segundo Hermano", EsCuota=true},
             new CargoCodigoValor { ID = (int)CargoCodigoValor.CargoCodigoValorIDs.CuartoHijo, Descripcion = "Tercer Hermano", EsCuota = true },
             new CargoCodigoValor { ID = (int)CargoCodigoValor.CargoCodigoValorIDs.QuintoHijo, Descripcion = "Cuarto Hermano", EsCuota = true },
             new CargoCodigoValor{ ID= (int)CargoCodigoValor.CargoCodigoValorIDs.Mora1, Descripcion="Primera Mora"},
             new CargoCodigoValor{ ID= (int)CargoCodigoValor.CargoCodigoValorIDs.Mora2, Descripcion="Segunda Mora"},
             new CargoCodigoValor{ ID= (int)CargoCodigoValor.CargoCodigoValorIDs.Comedor, Descripcion="Comedor"}             
            };
                CargoCodigos.ForEach(s => context.CargoCodigos.Add(s));
                context.SaveChanges();


                var calendarioVtos = new List<CalendarioVto> { 
                new CalendarioVto { ID = 1, Descripcion = "Default" }
            };
                calendarioVtos.ForEach(s => context.CalendarioVtos.Add(s));
                context.SaveChanges();
       

                var cursos = new List<Curso> { 
                new Curso { ID = 1, Codigo = "C1", Nivel = 1 } ,
                new Curso { ID = 2, Codigo = "C2", Nivel = 2 } ,
                new Curso { ID = 3, Codigo = "C3", Nivel = 3 } ,
                new Curso { ID = 4, Codigo = "C4", Nivel = 4 } ,
                new Curso { ID = 5, Codigo = "C5", Nivel = 5 } ,
                new Curso { ID = 6, Codigo = "C6", Nivel = 6 } ,
                new Curso { ID = 100, Codigo = "COM", Nivel = 1 } 
            };
                cursos.ForEach(s => context.Cursos.Add(s));
                context.SaveChanges();

                var familias = new List<Familia>{
                new Familia{   Descripcion="Ape 0" },
                new Familia{   Descripcion="Ape 1" },
                new Familia{   Descripcion="Ape 2" },
                new Familia{   Descripcion="Ape 3" },
                new Familia{   Descripcion="Ape 4" },
                new Familia{   Descripcion="Ape 5" },
                new Familia{   Descripcion="Ape 3" }
            };
                familias.ForEach(s => context.Familia.Add(s));
                context.SaveChanges();


                var alumnos = new List<Alumno> {
                new Alumno{  Apellido= "Ape 1", Nombre="Nombre 1", Familia= familias[1] },
                new Alumno{  Apellido= "Ape 2", Nombre="Nombre 2", Familia= familias[2] },
                new Alumno{  Apellido= "Ape 2", Nombre="Nombre 2 hermano", Familia= familias[2] , },
                new Alumno{  Apellido= "Ape 3", Nombre="Nombre 3", Familia= familias[3] },
                new Alumno{  Apellido= "Ape 4", Nombre="Nombre 4" , Familia= familias[4]},
                new Alumno{  Apellido= "Ape 5", Nombre="Nombre 5" , Familia= familias[5]},
                new Alumno{  Apellido= "Ape 3", Nombre="Nombre 3 Repetido", Familia= familias[6]},
            };
                alumnos.ForEach(s => context.Alumnos.Add(s));
                context.SaveChanges();

                var inscripciones = new List<Inscripcion>{
                new Inscripcion {  AlumnoID = 1, CursoID = 1, FechaAlta= DateTime.Today, CargoCodigoValorID= (int)CargoCodigoValor.CargoCodigoValorIDs.Completa },
                new Inscripcion {  AlumnoID = 2, CursoID = 1, FechaAlta= DateTime.Today, CargoCodigoValorID= (int)CargoCodigoValor.CargoCodigoValorIDs.Completa },
                new Inscripcion  {  AlumnoID = 3, CursoID = 1, FechaAlta= DateTime.Today, CargoCodigoValorID= (int)CargoCodigoValor.CargoCodigoValorIDs.SegundoHijo },
                new Inscripcion  {  AlumnoID = 4, CursoID = 1, FechaAlta= DateTime.Today, CargoCodigoValorID= (int)CargoCodigoValor.CargoCodigoValorIDs.Completa },
                new Inscripcion  {  AlumnoID = 5, CursoID = 2, FechaAlta= DateTime.Today, CargoCodigoValorID= (int)CargoCodigoValor.CargoCodigoValorIDs.Completa },
                new Inscripcion  {  AlumnoID = 6, CursoID = 2, FechaAlta= DateTime.Today, CargoCodigoValorID= (int)CargoCodigoValor.CargoCodigoValorIDs.Completa }
                };

                inscripciones.ForEach(s => context.Inscripciones.Add(s));
                context.SaveChanges();

                inscripciones = new List<Inscripcion>{
                new Inscripcion  {  AlumnoID = 1, CursoID = Curso.ComedorID , FechaAlta= DateTime.Today, CargoCodigoValorID= (int)CargoCodigoValor.CargoCodigoValorIDs.Comedor },
                new Inscripcion  {  AlumnoID = 4, CursoID = Curso.ComedorID, FechaAlta= DateTime.Today, CargoCodigoValorID= (int)CargoCodigoValor.CargoCodigoValorIDs.Comedor},
                new Inscripcion  {  AlumnoID = 5, CursoID = Curso.ComedorID, FechaAlta= DateTime.Today, CargoCodigoValorID= (int)CargoCodigoValor.CargoCodigoValorIDs.Comedor},
                new Inscripcion  {  AlumnoID = 6, CursoID = Curso.ComedorID, FechaAlta= DateTime.Today, CargoCodigoValorID= (int)CargoCodigoValor.CargoCodigoValorIDs.Comedor}
                };

                inscripciones.ForEach(s => context.Inscripciones.Add(s));
                context.SaveChanges();

                var valores = CargarValoresCursos( cursos);
                valores.ForEach(s => context.CargoValores.Add(s));
                context.SaveChanges();
            }
                
 

            catch (DataException ex)
            {

                throw ex;
            }

        }
        private List<CargoValor> CargarValoresCursos(List<Curso> lCursos)
        {
            List<CargoValor> lResult = new List<CargoValor>();
            lCursos = lCursos.Where(c => c.Codigo != "COM").ToList();
            // Where( c => c.Codigo != "COM" )
            // Completa
            lCursos.ForEach(c => lResult.Add(new CargoValor {  CursoID= c.ID , FechaDesde=new DateTime(2016,3,1), CargoCodigoValorID = (int)CargoCodigoValor.CargoCodigoValorIDs.Completa, Valor = 100 + c.Nivel}));
            //Primer hermano
            lCursos.ForEach(c => lResult.Add(new CargoValor { CursoID = c.ID, FechaDesde = new DateTime(2016, 3, 1), CargoCodigoValorID = (int)CargoCodigoValor.CargoCodigoValorIDs.SegundoHijo, Valor = (100 + c.Nivel) * .25 }));

            //Mora 1
            lCursos.ForEach(c => lResult.Add(new CargoValor { CursoID = c.ID, FechaDesde = new DateTime(2016, 3, 1), CargoCodigoValorID = (int)CargoCodigoValor.CargoCodigoValorIDs.Mora1, Valor = 30 + c.Nivel }));

            //Mora 2
            lCursos.ForEach(c => lResult.Add(new CargoValor { CursoID = c.ID, FechaDesde = new DateTime(2016, 3, 1), CargoCodigoValorID = (int)CargoCodigoValor.CargoCodigoValorIDs.Mora2, Valor = 30 + c.Nivel }));

            //comedor
            lResult.Add(new CargoValor { CursoID = 100, FechaDesde = new DateTime(2016, 3, 1), CargoCodigoValorID = (int)CargoCodigoValor.CargoCodigoValorIDs.Comedor, Valor = 100 });
            return lResult;
        }
    }
}