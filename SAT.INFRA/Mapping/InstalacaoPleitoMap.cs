using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class InstalacaoPleitoMap : IEntityTypeConfiguration<InstalacaoPleito>
    {
        public void Configure(EntityTypeBuilder<InstalacaoPleito> builder)
        {
            builder.ToTable("InstalPleito");

            builder
                .HasKey(i => new { i.CodInstalPleito });

            builder
               .HasOne(prop => prop.Contrato)
               .WithMany()
               .HasForeignKey(prop => prop.CodContrato)
               .HasPrincipalKey(prop => prop.CodContrato);

            builder
                .HasMany(prop => prop.InstalacoesPleitoInstal)
                .WithOne()
                .HasForeignKey(prop => prop.CodInstalPleito)
                .HasPrincipalKey(prop => prop.CodInstalPleito);
        }
    }
}