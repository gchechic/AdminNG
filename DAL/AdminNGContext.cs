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
        public DbSet<Recibo> Recibos{ get; set; }

        public DbSet<CuotaCodigo> CuotaCodigos{ get; set; }
        public DbSet<Sede> Sedes{ get; set; }

        public DbSet<FormaPago> FormaPagos { get; set; }

        public DbSet<FamiliaRol> FamiliaRoles { get; set; }


        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
            
        //}
    }
}