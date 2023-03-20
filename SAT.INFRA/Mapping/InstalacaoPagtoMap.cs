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

            builder
               .HasOne(prop => prop.Contrato)
               .WithMany()
               .HasForeignKey(prop => prop.CodContrato)
               .HasPrincipalKey(prop => prop.CodContrato);

            builder
                .HasMany(prop => prop.InstalacoesPagtoInstal)
                .WithOne()
                .HasForeignKey(prop => prop.CodInstalPagto)
                .HasPrincipalKey(prop => prop.CodInstalPagto);                
        }
    }
}