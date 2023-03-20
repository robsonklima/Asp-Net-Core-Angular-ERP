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
                .HasKey(i => new { i.CodInstalacao, i.CodInstalPagto, i.CodInstalTipoParcela });           

            builder
                .HasOne(prop => prop.Instalacao)
                .WithMany()
                .HasForeignKey(prop => prop.CodInstalacao)
                .HasPrincipalKey(prop => prop.CodInstalacao);   

            builder
               .HasOne(prop => prop.InstalacaoTipoParcela)
               .WithMany()
               .HasForeignKey(prop => prop.CodInstalTipoParcela)
               .HasPrincipalKey(prop => prop.CodInstalTipoParcela);         

            builder
               .HasOne(prop => prop.InstalacaoMotivoMulta)
               .WithMany()
               .HasForeignKey(prop => prop.CodInstalMotivoMulta)
               .HasPrincipalKey(prop => prop.CodInstalMotivoMulta);                                                                                      
        }
    }
}