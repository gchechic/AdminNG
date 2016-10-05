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
                var sedes = new List<Sede>
            {
                new Sede{ ID = (int)Sede.IDS.Bulnes, Nombre ="Bulnes", ResponsableID=10},
                new Sede{ ID = (int)Sede.IDS.Ugarteche, Nombre ="Ugarteche", ResponsableID=11}
            };
                context.Sedes.AddRange(sedes);
                context.SaveChanges();

                var responsables = new List<Responsable>
            {
                new Responsable{ ID = 1, Nombre ="Responsable 1", Grade= Grade.A},
                new Responsable{ ID = 2, Nombre ="Responsable 2", Grade= Grade.C},
                new Responsable{ ID = 3, Nombre ="Responsable 3", Grade= Grade.C},
                new Responsable{ ID = 10, Nombre = context.Sedes.Find((int)Sede.IDS.Bulnes).Nombre, Grade= Grade.R},
                new Responsable{ ID = 11, Nombre = context.Sedes.Find((int)Sede.IDS.Ugarteche).Nombre,  Grade= Grade.R}
            };
                context.Responsables.AddRange(responsables);
                context.SaveChanges();


                var formapagos = new List<FormaPago>
            {
                new FormaPago{ID=(int)FormaPago.IDS.Contado, Descripcion="Contado" },
                new FormaPago{ID=(int)FormaPago.IDS.Bancario, Descripcion="Bancario" }
            };
                context.FormaPagos.AddRange(formapagos);
                context.SaveChanges();

               
                var movimientoCuentaTipos = new List<MovimientoCuentaTipo>
            {
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.Cuota, Descripcion="Cuota" },
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.GtoAdm, Descripcion="GtoAdm" },
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.Comedor, Descripcion="Comedor" },
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.Pago, Descripcion="Pago" },
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.Matricula, Descripcion="Matricula" },
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.Bonificacion, Descripcion="Bonificacion" },
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.Redondeo, Descripcion="Redondeo" }
            };
                context.MovimientoCuentaTipos.AddRange(movimientoCuentaTipos);
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
             new CargoCodigoValor{ ID= (int)CargoCodigoValor.IDS.Completa, Descripcion = "Completa", EsCuota=true},
             new CargoCodigoValor{ ID= (int)CargoCodigoValor.IDS.SegundoHijo, Descripcion="Primer Hermano", EsCuota=true},
             new CargoCodigoValor{ ID= (int)CargoCodigoValor.IDS.TercerHijo, Descripcion="Segundo Hermano", EsCuota=true},
             new CargoCodigoValor { ID = (int)CargoCodigoValor.IDS.CuartoHijo, Descripcion = "Tercer Hermano", EsCuota = true },
             new CargoCodigoValor { ID = (int)CargoCodigoValor.IDS.QuintoHijo, Descripcion = "Cuarto Hermano", EsCuota = true },
             new CargoCodigoValor{ ID= (int)CargoCodigoValor.IDS.GtoAdm1, Descripcion="Primera GtoAdm"},
             new CargoCodigoValor{ ID= (int)CargoCodigoValor.IDS.GtoAdm2, Descripcion="Segunda GtoAdm"},
              new CargoCodigoValor{ ID= (int)CargoCodigoValor.IDS.Comedor, Descripcion="Codigo de Comedor"},
             new CargoCodigoValor{ ID= (int)CargoCodigoValor.IDS.Comedorx2, Descripcion="Comedor 2 veces"},
             new CargoCodigoValor{ ID= (int)CargoCodigoValor.IDS.Comedorx3, Descripcion="Comedor 3 veces"}
            };
                CargoCodigos.ForEach(s => context.CargoCodigos.Add(s));
                context.SaveChanges();


                var calendarioVtos = new List<CalendarioVto> { 
                new CalendarioVto { ID = 1, Descripcion = "Default" },
                new CalendarioVto { ID = 2, Descripcion = "Mas 2" }
            };
                context.CalendarioVtos.AddRange(calendarioVtos);
                context.SaveChanges();

                var calendarioVtosItems= new List<CalendarioVtoItem > { 
                new CalendarioVtoItem {  CalendarioVtoID = 1, Mes=3, PrimerVto =new DateTime(2016,3,5), SegundoVto= new DateTime(2016,3,15)  },
                new CalendarioVtoItem {  CalendarioVtoID = 1, Mes=4, PrimerVto =new DateTime(2016,4,5), SegundoVto= new DateTime(2016,4,15)  },
                new CalendarioVtoItem {  CalendarioVtoID = 1, Mes=5, PrimerVto =new DateTime(2016,5,5), SegundoVto= new DateTime(2016,5,15)  },
                new CalendarioVtoItem {  CalendarioVtoID = 1, Mes=6, PrimerVto =new DateTime(2016,6,5), SegundoVto= new DateTime(2016,6,15)  },
                new CalendarioVtoItem {  CalendarioVtoID = 1, Mes=7, PrimerVto =new DateTime(2016,7,5), SegundoVto= new DateTime(2016,7,15)  },
                new CalendarioVtoItem {  CalendarioVtoID = 1, Mes=8, PrimerVto =new DateTime(2016,8,5), SegundoVto= new DateTime(2016,8,15)  },
                new CalendarioVtoItem {  CalendarioVtoID = 1, Mes=9, PrimerVto =new DateTime(2016,9,5), SegundoVto= new DateTime(2016,9,15)  },
                new CalendarioVtoItem {  CalendarioVtoID = 1, Mes=10, PrimerVto =new DateTime(2016,10,5), SegundoVto= new DateTime(2016,10,15)  },
                new CalendarioVtoItem {  CalendarioVtoID = 1, Mes=11, PrimerVto =new DateTime(2016,11,5), SegundoVto= new DateTime(2016,11,15)  },
                new CalendarioVtoItem {  CalendarioVtoID = 1, Mes=12, PrimerVto =new DateTime(2016,12,5), SegundoVto= new DateTime(2016,12,15)  }
            };
                context.CalendarioVtoItems.AddRange(calendarioVtosItems);
                context.SaveChanges();

                calendarioVtosItems = new List<CalendarioVtoItem> { 
                new CalendarioVtoItem {  CalendarioVtoID = 2, Mes=3, PrimerVto =new DateTime(2016,3,7), SegundoVto= new DateTime(2016,3,17)  },
                new CalendarioVtoItem {  CalendarioVtoID = 2, Mes=4, PrimerVto =new DateTime(2016,4,7), SegundoVto= new DateTime(2016,4,17)  },
                new CalendarioVtoItem {  CalendarioVtoID = 2, Mes=5, PrimerVto =new DateTime(2016,5,7), SegundoVto= new DateTime(2016,5,17)  },
                new CalendarioVtoItem {  CalendarioVtoID = 2, Mes=6, PrimerVto =new DateTime(2016,6,7), SegundoVto= new DateTime(2016,6,17)  },
                new CalendarioVtoItem {  CalendarioVtoID = 2, Mes=7, PrimerVto =new DateTime(2016,7,7), SegundoVto= new DateTime(2016,7,17)  },
                new CalendarioVtoItem {  CalendarioVtoID = 2, Mes=8, PrimerVto =new DateTime(2016,8,7), SegundoVto= new DateTime(2016,8,17)  },
                new CalendarioVtoItem {  CalendarioVtoID = 2, Mes=9, PrimerVto =new DateTime(2016,9,7), SegundoVto= new DateTime(2016,9,17)  },
                new CalendarioVtoItem {  CalendarioVtoID = 2, Mes=10, PrimerVto =new DateTime(2016,10,7), SegundoVto= new DateTime(2016,10,17)  },
                new CalendarioVtoItem {  CalendarioVtoID = 2, Mes=11, PrimerVto =new DateTime(2016,11,7), SegundoVto= new DateTime(2016,11,17)  },
                new CalendarioVtoItem {  CalendarioVtoID = 2, Mes=12, PrimerVto =new DateTime(2016,12,7), SegundoVto= new DateTime(2016,12,17)  }
            };
                context.CalendarioVtoItems.AddRange(calendarioVtosItems);
                context.SaveChanges();


                //var cursos = new List<Curso> {

                //new Curso { ID = 1002, Codigo = "B2 A", Nivel = 2 } ,
                //new Curso { ID = 1003, Codigo = "SK A", Nivel = 0 } ,
                //new Curso { ID = 1004, Codigo = "B1 A", Nivel = 1 } ,
                //new Curso { ID = 1005, Codigo = "B1 E", Nivel = 1 } ,
                //new Curso { ID = 1006, Codigo = "B1 M", Nivel = 1 } ,
                //new Curso { ID = 1007, Codigo = "B2 E", Nivel = 2 } ,
                //new Curso { ID = 1008, Codigo = "B2 M", Nivel = 2 } ,
                //new Curso { ID = 1009, Codigo = "B3 E", Nivel = 3 } ,
                //new Curso { ID = 1010, Codigo = "B3 M", Nivel = 3 } ,
                //new Curso { ID = 1011, Codigo = "INT 1 E", Nivel = 7 } ,
                //new Curso { ID = 1012, Codigo = "INT 2 E", Nivel = 8 } ,
                //new Curso { ID = 1013, Codigo = "INT 3 E", Nivel = 9 } ,
                //new Curso { ID = 1014, Codigo = "JR 1 E", Nivel = 4 } ,
                //new Curso { ID = 1015, Codigo = "JR 1 M", Nivel = 4 } ,
                //new Curso { ID = 1016, Codigo = "JR 2 E", Nivel = 5 } ,
                //new Curso { ID = 1017, Codigo = "JR 2 M", Nivel = 5 } ,
                //new Curso { ID = 1018, Codigo = "JR 3 E", Nivel = 6 } ,
                //new Curso { ID = 1019, Codigo = "JR 3 M", Nivel = 6 } ,
                //new Curso { ID = 1020, Codigo = "SK M", Nivel = 0 } ,
                //new Curso { ID = 1021, Codigo = "SR 1 E", Nivel = 10 } ,
                //new Curso { ID = 1022, Codigo = "SR 2 E", Nivel = 11 } ,
                //new Curso { ID = 1023, Codigo = "SR 3 E", Nivel = 12 } ,

                //new Curso { ID = 100, Codigo = "COM", Nivel = 0 } 
                //};

                //cursos.ForEach(s => context.Cursos.Add(s));
                //context.SaveChanges();

            //    var familias = new List<Familia>{
            //    new Familia{   Descripcion="Ape 0" },
            //    new Familia{   Descripcion="Ape 1" },
            //    new Familia{   Descripcion="Ape 2" },
            //    new Familia{   Descripcion="Ape 3" },
            //    new Familia{   Descripcion="Ape 4" },
            //    new Familia{   Descripcion="Ape 5" },
            //    new Familia{   Descripcion="Ape 3" }
            //};
            //    familias.ForEach(s => context.Familia.Add(s));
            //    context.SaveChanges();


            //    var alumnos = new List<Alumno> {
            //    new Alumno{  Apellido= "Ape 1", Nombre="Nombre 1", Familia= familias[1] },
            //    new Alumno{  Apellido= "Ape 2", Nombre="Nombre 2", Familia= familias[2] },
            //    new Alumno{  Apellido= "Ape 2", Nombre="Nombre 2 hermano", Familia= familias[2] , },
            //    new Alumno{  Apellido= "Ape 3", Nombre="Nombre 3", Familia= familias[3] },
            //    new Alumno{  Apellido= "Ape 4", Nombre="Nombre 4" , Familia= familias[4]},
            //    new Alumno{  Apellido= "Ape 5", Nombre="Nombre 5" , Familia= familias[5]},
            //    new Alumno{  Apellido= "Ape 3", Nombre="Nombre 3 Repetido", Familia= familias[6]},
            //};
            //    alumnos.ForEach(s => context.Alumnos.Add(s));
            //    context.SaveChanges();

            //    DateTime FechaAlta = new DateTime(2016, 3, 1);
            //    var inscripciones = new List<Inscripcion>{
            //    new Inscripcion {  AlumnoID = 1, CursoID = 1, FechaAlta= FechaAlta, CargoCodigoValorID= (int)CargoCodigoValor.IDS.Completa },
            //    new Inscripcion {  AlumnoID = 2, CursoID = 1, FechaAlta= FechaAlta, CargoCodigoValorID= (int)CargoCodigoValor.IDS.Completa },
            //    new Inscripcion  {  AlumnoID = 3, CursoID = 1, FechaAlta= FechaAlta, CargoCodigoValorID= (int)CargoCodigoValor.IDS.SegundoHijo },
            //    new Inscripcion  {  AlumnoID = 4, CursoID = 1, FechaAlta= FechaAlta, CargoCodigoValorID= (int)CargoCodigoValor.IDS.Completa },
            //    new Inscripcion  {  AlumnoID = 5, CursoID = 2, FechaAlta= FechaAlta, CargoCodigoValorID= (int)CargoCodigoValor.IDS.Completa },
            //    new Inscripcion  {  AlumnoID = 6, CursoID = 2, FechaAlta= FechaAlta, CargoCodigoValorID= (int)CargoCodigoValor.IDS.Completa, CalendarioVtoID=2}
            //    };

            //    inscripciones.ForEach(s => context.Inscripciones.Add(s));
            //    context.SaveChanges();

            //    inscripciones = new List<Inscripcion>{
            //    new Inscripcion  {  AlumnoID = 1, CursoID = (int)Curso.IDS.Comedor, FechaAlta= FechaAlta, CargoCodigoValorID= (int)CargoCodigoValor.IDS.Comedor },
            //    new Inscripcion  {  AlumnoID = 4, CursoID = (int)Curso.IDS.Comedor, FechaAlta= FechaAlta, CargoCodigoValorID= (int)CargoCodigoValor.IDS.Comedor},
            //    new Inscripcion  {  AlumnoID = 5, CursoID = (int)Curso.IDS.Comedor, FechaAlta= FechaAlta, CargoCodigoValorID= (int)CargoCodigoValor.IDS.Comedor},
            //    new Inscripcion  {  AlumnoID = 6, CursoID = (int)Curso.IDS.Comedor, FechaAlta= FechaAlta, CargoCodigoValorID= (int)CargoCodigoValor.IDS.Comedor}
            //    };

            //    inscripciones.ForEach(s => context.Inscripciones.Add(s));
            //    context.SaveChanges();

            //    var valores = CargarValoresCursos( cursos, new DateTime(2016,3,1));
            //    valores.ForEach(s => context.CargoValores.Add(s));
            //    context.SaveChanges();

                //valores = CargarValoresCursos(cursos, new DateTime(2016, 5, 1));
                //valores.ForEach(s => context.CargoValores.Add(s));
                //context.SaveChanges();
            }
                
 

            catch (DataException ex)
            {

                throw ex;
            }

        }
        private List<CargoValor> CargarValoresCursos(List<Curso> lCursos, DateTime Fecha)
        {
            List<CargoValor> lResult = new List<CargoValor>();
            lCursos = lCursos.Where(c => c.Codigo != "COM").ToList();
            double valorCuota = 100 + Fecha.Month, valorGtoAdm1 = 30 + Fecha.Month, valorGtoAdm2 = 40 + Fecha.Month, valorComedor = 50 + Fecha.Month;
            // Where( c => c.Codigo != "COM" )
            // Completa
            lCursos.ForEach(c => lResult.Add(new CargoValor { CursoID = c.ID, FechaDesde = Fecha, CargoCodigoValorID = (int)CargoCodigoValor.IDS.Completa, Valor = valorCuota + c.Nivel }));
            //Primer hermano
            lCursos.ForEach(c => lResult.Add(new CargoValor { CursoID = c.ID, FechaDesde = Fecha, CargoCodigoValorID = (int)CargoCodigoValor.IDS.SegundoHijo, Valor = (valorCuota + c.Nivel) * .25 }));

            //GtoAdm 1
            lCursos.ForEach(c => lResult.Add(new CargoValor { CursoID = c.ID, FechaDesde = Fecha, CargoCodigoValorID = (int)CargoCodigoValor.IDS.GtoAdm1, Valor = valorGtoAdm1 + c.Nivel }));

            //GtoAdm 2
            lCursos.ForEach(c => lResult.Add(new CargoValor { CursoID = c.ID, FechaDesde = Fecha, CargoCodigoValorID = (int)CargoCodigoValor.IDS.GtoAdm2, Valor = valorGtoAdm2 + c.Nivel }));

            //comedor
            lResult.Add(new CargoValor { CursoID = 100, FechaDesde = Fecha, CargoCodigoValorID = (int)CargoCodigoValor.IDS.Completa, Valor = valorComedor });
            return lResult;
        }
    }
}