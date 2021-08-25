using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class RegiaoAutorizadaMap : IEntityTypeConfiguration<RegiaoAutorizada>
    {
        public void Configure(EntityTypeBuilder<RegiaoAutorizada> builder)
        {
            builder.ToTable("RegiaoAutorizada");

            builder.HasKey(prop => prop.CodFilial);
            builder.HasKey(prop => prop.CodRegiao);
            builder.HasKey(prop => prop.CodAutorizada);
        }
    }
}
