using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class RecursoBloqueadoMap : IEntityTypeConfiguration<RecursoBloqueado>
    {
        public void Configure(EntityTypeBuilder<RecursoBloqueado> builder)
        {
            builder.ToTable("RecursosBloqueados");
            builder.HasKey(prop => prop.CodRecursoBloqueado);
        }
    }
}