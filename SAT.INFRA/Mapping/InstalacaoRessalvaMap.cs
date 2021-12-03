using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class InstalacaoRessalvaMap : IEntityTypeConfiguration<InstalacaoRessalva>
    {
        public void Configure(EntityTypeBuilder<InstalacaoRessalva> builder)
        {
            builder.ToTable("InstalacaoRessalva");

            builder
                .HasKey(i => new { i.CodInstalRessalva });

            builder
                .HasOne(i => i.InstalacaoMotivoRes)
                .WithOne()
                .HasForeignKey<InstalacaoMotivoRes>(i => i.CodInstalMotivoRes)
                .HasPrincipalKey<InstalacaoRessalva>(i => i.CodInstalMotivoRes);
                                                       
        }
    }
}
