using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ImprodutividadeMap : IEntityTypeConfiguration<Improdutividade>
    {
        public void Configure(EntityTypeBuilder<Improdutividade> builder)
        {
            builder
                .ToTable("Improdutividade");

            builder
                .HasKey(prop => prop.CodImprodutividade);
        }
    }
}