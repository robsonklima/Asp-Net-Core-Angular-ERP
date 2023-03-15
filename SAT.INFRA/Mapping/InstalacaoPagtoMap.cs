using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class InstalacaoPagtoMap : IEntityTypeConfiguration<InstalacaoPagto>
    {
        public void Configure(EntityTypeBuilder<InstalacaoPagto> builder)
        {
            builder.ToTable("InstalPagto");

            builder
                .HasKey(i => new { i.CodInstalPagto });
        }
    }
}