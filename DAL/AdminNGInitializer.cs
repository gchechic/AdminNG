using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminNG.Models;
using System.Data;
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
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.PrimeraMora, Descripcion="Primera Mora" },
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.SegundaMora, Descripcion="Segunda Mora" },
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.Matricula, Descripcion="Matricula" },
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.Comedor, Descripcion="Comedor" },
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.Pago, Descripcion="Pago" },
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.Bonificacion, Descripcion="Bonificacion" },
                new MovimientoCuentaTipo{ID=(int)MovimientoCuentaTipo.IDS.Redondeo, Descripcion="Redondeo" }
            };
                movimientoCuentaTipos.ForEach(s => context.CargoTipos.Add(s));
                context.SaveChanges();


                var familiaroles = new List<FamiliaRol>
            {
                new FamiliaRol{ID=1, Descripcion="Padre" },
                new FamiliaRol{ID=2, Descripcion="Madre" }                
            };
                familiaroles.ForEach(s => context.FamiliaRoles.Add(s));
                context.SaveChanges();

                var cuotacodigos = new List<CargoCodigo>
            {
             new CargoCodigo{ ID= (int)CargoCodigo.CargoCodigoIDs.Completa, Descripcion = "Completa"},
             new CargoCodigo{ ID= (int)CargoCodigo.CargoCodigoIDs.SegundoHijo, Descripcion="Primer Hermano"},
             new CargoCodigo{ ID= (int)CargoCodigo.CargoCodigoIDs.TercerHijo, Descripcion="Segundo Hermano"},
             new CargoCodigo{ ID= (int)CargoCodigo.CargoCodigoIDs.CuartoHijo, Descripcion="Tercer Hermano"},
             new CargoCodigo{ ID= (int)CargoCodigo.CargoCodigoIDs.QuintoHijo, Descripcion="Cuarto Hermano"},
             new CargoCodigo{ ID= (int)CargoCodigo.CargoCodigoIDs.Completa, Descripcion="Primera Mora"},
             new CargoCodigo{ ID= (int)CargoCodigo.CargoCodigoIDs.Completa, Descripcion="Segunda Mora"}             
            };
                cuotacodigos.ForEach(s => context.CuotaCodigos.Add(s));
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
                new Curso { ID = 100, Codigo = "COM", Nivel = 7 } 
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
                new Alumno{  Apellido= "Ape 1", Nombre="Nombre 1", Familia= familias[1] , Comedor=true},
                new Alumno{  Apellido= "Ape 2", Nombre="Nombre 2", Familia= familias[2] , Comedor=true},
                new Alumno{  Apellido= "Ape 2", Nombre="Nombre 2 hermano", Familia= familias[2] , Comedor=true},
                new Alumno{  Apellido= "Ape 3", Nombre="Nombre 3", Familia= familias[3] , Comedor=false},
                new Alumno{  Apellido= "Ape 4", Nombre="Nombre 4" , Familia= familias[4], Comedor=true},
                new Alumno{  Apellido= "Ape 5", Nombre="Nombre 5" , Familia= familias[5], Comedor=false},
                new Alumno{  Apellido= "Ape 3", Nombre="Nombre 3 Repetido", Familia= familias[6], Comedor=true },
            };
                alumnos.ForEach(s => context.Alumnos.Add(s));
                context.SaveChanges();

                var inscripciones = new List<Inscripcion>{
                new Inscripcion {  AlumnoID = 1, CursoID = 1, FechaAlta= DateTime.Now, CuotaCodigoID= (int)CargoCodigo.CargoCodigoIDs.Completa },
                new Inscripcion {  AlumnoID = 2, CursoID = 1, FechaAlta= DateTime.Now, CuotaCodigoID= (int)CargoCodigo.CargoCodigoIDs.Completa },
                new Inscripcion  {  AlumnoID = 3, CursoID = 1, FechaAlta= DateTime.Now, CuotaCodigoID= (int)CargoCodigo.CargoCodigoIDs.SegundoHijo },
                new Inscripcion  {  AlumnoID = 4, CursoID = 1, FechaAlta= DateTime.Now, CuotaCodigoID= (int)CargoCodigo.CargoCodigoIDs.Completa },
                new Inscripcion  {  AlumnoID = 5, CursoID = 2, FechaAlta= DateTime.Now, CuotaCodigoID= (int)CargoCodigo.CargoCodigoIDs.Completa },
                new Inscripcion  {  AlumnoID = 6, CursoID = 2, FechaAlta= DateTime.Now, CuotaCodigoID= (int)CargoCodigo.CargoCodigoIDs.Completa }
                };
               // alumnos.ForEach(s => inscripciones.Add(new Inscripcion { Alumno = s, Curso = cursos.Find(c => c.ID == 7-s.ID), FechaAlta = DateTime.Now }));

                inscripciones.ForEach(s => context.Inscripciones.Add(s));
                context.SaveChanges();

            }


            catch (DataException ex)
            {

                throw ex;
            }

        }
        
    }
}