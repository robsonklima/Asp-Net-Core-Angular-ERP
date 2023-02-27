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

            builder
                .HasOne(prop => prop.Instalacao)
                .WithMany()
                .HasForeignKey(prop => prop.CodInstalacao)
                .HasPrincipalKey(prop => prop.CodInstalacao);
            
            builder
                .HasOne(prop => prop.InstalacaoPleito)
                .WithMany()
                .HasForeignKey(prop => prop.CodInstalPleito)
                .HasPrincipalKey(prop => prop.CodInstalPleito);                                                
        }
    }
}