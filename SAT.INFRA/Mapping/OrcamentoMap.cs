using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrcamentoMap : IEntityTypeConfiguration<Orcamento>
    {
        public void Configure(EntityTypeBuilder<Orcamento> builder)
        {
            builder.ToTable("Orc");
            builder.HasKey(prop => prop.CodOrc);
            builder
              .HasOne(i => i.OrdemServico)
              .WithOne()
              .HasForeignKey<OrdemServico>(i => i.CodOS)
              .HasPrincipalKey<Orcamento>(i => i.CodigoOrdemServico);
        }
    }
}