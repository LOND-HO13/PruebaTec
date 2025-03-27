using Incidencias.Core.Models;
using Incidencias.Data.Configurations;
using System.Data.Entity;

namespace Incidencias.Data
{
    public class IncidenciasDbContext : DbContext
    {
        public IncidenciasDbContext() : base("name=IncidenciasConnection")
        {
            // Configuración inicial
            Database.SetInitializer(new CreateDatabaseIfNotExists<IncidenciasDbContext>());
        }

        public DbSet<Incidencia> Incidencias { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Aplicar configuraciones Fluent API
            modelBuilder.Configurations.Add(new IncidenciaConfiguration());

            modelBuilder.Entity<Tecnico>().ToTable("Tecnicoes");

            modelBuilder.Entity<Incidencia>()
            .HasRequired(i => i.TecnicoAsignado)
            .WithMany()
            .HasForeignKey(i => i.TecnicoAsignadoId)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Incidencia>()
                .HasMany(i => i.Comentarios)
                .WithRequired(c => c.Incidencia)
                .HasForeignKey(c => c.IncidenciaId);

            base.OnModelCreating(modelBuilder);
        }
    }
}