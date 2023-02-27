using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class InstalacaoPleitoInstalMap : IEntityTypeConfiguration<InstalacaoPleitoInstal>
    {
        public void Configure(EntityTypeBuilder<InstalacaoPleitoInstal> builder)
        {
            builder.ToTable("InstalPleitoInstal");

            builder
                .HasKey(i => new { i.CodInstalacao, i.CodInstalPleito });                                     
        }
    }
}