using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OsBancadaPecasOrcamentoMap : IEntityTypeConfiguration<OsBancadaPecasOrcamento>
    {
        public void Configure(EntityTypeBuilder<OsBancadaPecasOrcamento> builder)
        {
            builder
                .ToTable("OsbancadaPecasOrcamento");

            builder
                .HasKey(i => i.CodOrcamento);

            builder
                .HasOne(prop => prop.OSBancada)
                .WithMany()
                .HasForeignKey(prop => prop.CodOsbancada)
                .HasPrincipalKey(prop => prop.CodOsbancada);
            
            builder
                .HasOne(prop => prop.PecaRE5114)
                .WithMany()
                .HasForeignKey(prop => prop.CodPecaRe5114)
                .HasPrincipalKey(prop => prop.CodPecaRe5114);

        }
    }
}