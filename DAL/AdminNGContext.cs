using AdminNG.Models;
using AdminNG.Models.CtaCte;
using AdminNG.Models.Pagos;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AdminNG.DAL
{
    public class AdminNGContext : DbContext
    {
        public AdminNGContext()
            : base("AdminNGContext")
        {
        }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Responsable> Responsables { get; set; }
        public DbSet<CalendarioVto> CalendarioVtos { get; set; }
        public DbSet<CargoCodigoValor> CargoCodigos { get; set; }
        public DbSet<Sede> Sedes { get; set; }
        public DbSet<MovimientoCuentaTipo> MovimientoCuentaTipos { get; set; }


        public DbSet<Familia> Familia { get; set; }
        public DbSet<FamiliaRol> FamiliaRoles { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }

        public System.Data.Entity.DbSet<Pago> Pagos { get; set; }
        public DbSet<PagoContado> PagoContados { get; set; }
        public DbSet<PagoBancario> PagoBancarios { get; set; }
        // public DbSet<MovimientoPago> MovimientoPagos { get; set; }
        public DbSet<FormaPago> FormaPagos { get; set; }


        public DbSet<MovimientoCuenta> MovimientoCuentas { get; set; }
        public DbSet<MovimientoCargo> MovimientoCargos { get; set; }
        public DbSet<CargoCuota> CargoCuotas { get; set; }
        public DbSet<CargoComedor> CargoComedores { get; set; }
        public DbSet<CargoGtoAdm> CargoGtoAdms { get; set; }


        public DbSet<CargoValor> CargoValores { get; set; }

        public DbSet<CalendarioVtoItem> CalendarioVtoItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<MovimientoCuenta>()
            //.Map<MovimientoComedor>(m => m.Requires("Codigo2").HasValue("COM"))
            //.Map<MovimientoCuota>(m => m.Requires("Codigo").HasValue("CUO"));
            // base.OnModelCreating(modelBuilder);
            //     modelBuilder.Entity<Menu>()
            //.HasRequired(f => f.Status)
            //.WithRequiredDependent()
            //.WillCascadeOnDelete(false);
            /*
             * 
             * modelBuilder.Entity<...>()
                        .HasRequired(...)
                        .WithMany(...)
                        .HasForeignKey(...)
                        .WillCascadeOnDelete(false);
             */
            //modelBuilder.Entity<MovimientoCargo>()
            //   .HasRequired(f => f.Inscripcion)
            //   .WithOptional()
            //   .WillCascadeOnDelete(false);

            ///TODO NO SE QUE HICE ACA PERO ANDUVO
            //modelBuilder.Entity<MovimientoCargo>().HasOptional(f => f.Inscripcion).WithRequired().WillCascadeOnDelete(false);


            modelBuilder.Entity<Pago>().ToTable("Pagos");
            // modelBuilder.Entity<MovimientoCargo>().ToTable("Cargos"); 
            modelBuilder.Entity<MovimientoCargo>()
    .HasRequired(c => c.Inscripcion).WithMany()
    .WillCascadeOnDelete(false);


            modelBuilder.Entity<Imputacion>()
             .HasRequired(c => c.MovimientoDebe).WithMany()
             .WillCascadeOnDelete(false);
            modelBuilder.Entity<Imputacion>()
 .HasRequired(c => c.MovimientoHaber).WithMany()
 .WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<AdminNG.Models.Caja.MovimientoCaja> MovimientoCajas { get; set; }

        public System.Data.Entity.DbSet<AdminNG.Models.Caja.MovimientoCajaTipo> MovimientoCajaTipos { get; set; }


        public System.Data.Entity.DbSet<AdminNG.Models.Proceso> Procesos { get; set; }


        public System.Data.Entity.DbSet<AdminNG.Models.CtaCte.Imputacion> Imputaciones { get; set; }


    }
}
