using Incidencias.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace Incidencias.Data.Configurations
{
    public class IncidenciaConfiguration : EntityTypeConfiguration<Incidencia>
    {
        public IncidenciaConfiguration()
        {
            // Configuración de relaciones
            HasRequired(i => i.UsuarioReporta)
                .WithMany()
                .HasForeignKey(i => i.UsuarioReportaId)
                .WillCascadeOnDelete(false);

            HasRequired(i => i.TecnicoAsignado)
                .WithMany()
                .HasForeignKey(i => i.TecnicoAsignadoId)
                .WillCascadeOnDelete(false);

            // Configuración de campos
            Property(i => i.Titulo).HasMaxLength(100);
        }
    }
}