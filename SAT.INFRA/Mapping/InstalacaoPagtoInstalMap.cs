using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class InstalacaoPagtoInstalMap : IEntityTypeConfiguration<InstalacaoPagtoInstal>
    {
        public void Configure(EntityTypeBuilder<InstalacaoPagtoInstal> builder)
        {
            builder.ToTable("InstalPagtoInstal");

            builder
                .HasKey(i => new { i.CodInstalacao, i.CodInstalPagto });                                         
        }
    }
}