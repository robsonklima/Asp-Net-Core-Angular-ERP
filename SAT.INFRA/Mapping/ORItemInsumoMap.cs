using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ORItemInsumoMap : IEntityTypeConfiguration<ORItemInsumo>
    {
        public void Configure(EntityTypeBuilder<ORItemInsumo> builder)
        {
            builder.ToTable("ORItemInsumo");
            builder.HasKey(i => i.CodORItemInsumo);
        }
    }
}