using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class RelatorioAtendimentoPOSMap : IEntityTypeConfiguration<RelatorioAtendimentoPOS>
    {
        public void Configure(EntityTypeBuilder<RelatorioAtendimentoPOS> builder)
        {
            builder.ToTable("RatBanrisul");
            builder.HasKey(i => new { i.CodRat });
        }
    }
}