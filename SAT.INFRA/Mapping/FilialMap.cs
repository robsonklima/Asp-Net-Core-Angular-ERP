using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class FilialMap : IEntityTypeConfiguration<Filial>
    {
        public void Configure(EntityTypeBuilder<Filial> builder)
        {
            builder
                .ToTable("Filial");

            builder
                .HasKey(prop => prop.CodFilial);

            // builder
            //     .HasOne(prop => prop.Cidade)
            //     .WithMany()
            //     .HasForeignKey("CodCidade")
            //     .HasPrincipalKey("CodCidade");

            builder
                .HasOne(prop => prop.FilialAnalista)
                .WithOne()
                .HasForeignKey<FilialAnalista>(p => p.CodFilial)
                .HasPrincipalKey<Filial>(p => p.CodFilial);

            builder
               .HasOne(prop => prop.OrcamentoISS)
               .WithOne()
               .HasForeignKey<OrcamentoISS>(p => p.CodigoFilial)
               .HasPrincipalKey<Filial>(p => p.CodFilial);
        }
    }
}