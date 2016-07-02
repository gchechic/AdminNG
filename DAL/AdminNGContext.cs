using AdminNG.Models;
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
        
        public DbSet<Alumno> Alumnos{ get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<Curso> Cursos{ get; set; }
        

        public DbSet<CuotaCodigo> CuotaCodigos{ get; set; }
        public DbSet<Sede> Sedes{ get; set; }

        public DbSet<FormaPago> FormaPagos { get; set; }

        public DbSet<Familia> Familia { get; set; }
        public DbSet<FamiliaRol> FamiliaRoles { get; set; }
        
        public DbSet<MovimientoCuentaTipo> CargoTipos { get; set; }

        public DbSet<Responsable> Responsables { get; set; }        

        public DbSet<MovimientoPago> MovimientoPagos { get; set; }

        public DbSet<PagoContado> PagoContados { get; set; }
        public DbSet<PagoBancario> PagoBancarios { get; set; }
    

        public DbSet<MovimientoCuenta> MovimientoCuentas { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<MovimientoCuenta>()
           //.Map<MovimientoComedor>(m => m.Requires("Codigo2").HasValue("COM"))
           //.Map<MovimientoCuota>(m => m.Requires("Codigo").HasValue("CUO"));
           // base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Pago>().ToTable("Pagos");   
        }

        public System.Data.Entity.DbSet<AdminNG.Models.Pago> Pagos { get; set; }
    }
}