using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class SequenciaMap : IEntityTypeConfiguration<Sequencia>
    {
        public void Configure(EntityTypeBuilder<Sequencia> builder)
        {
            builder.ToTable("Sequencia");

            builder.HasKey(prop => prop.Coluna);
            builder.HasKey(prop => prop.Tabela);
        }
    }
}