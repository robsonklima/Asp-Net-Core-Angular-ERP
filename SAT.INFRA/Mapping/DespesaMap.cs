using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class DespesaMap : IEntityTypeConfiguration<Despesa>
    {
        public void Configure(EntityTypeBuilder<Despesa> builder)
        {
            builder.ToTable("Despesa");
            builder.HasKey(i => i.CodDespesa);

            builder
                .HasOne(prop => prop.DespesaPeriodo)
                .WithMany()
                .HasForeignKey(prop => prop.CodDespesaPeriodo)
                .HasPrincipalKey(prop => prop.CodDespesaPeriodo);

            builder
                .HasOne(prop => prop.RelatorioAtendimento)
                .WithMany()
                .HasForeignKey(prop => prop.CodRAT)
                .HasPrincipalKey(prop => prop.CodRAT);

            builder
                .HasOne(prop => prop.Filial)
                .WithMany()
                .HasForeignKey(prop => prop.CodFilial)
                .HasPrincipalKey(prop => prop.CodFilial);

            builder
                .HasMany(prop => prop.DespesaItens)
                .WithOne()
                .HasForeignKey(prop => prop.CodDespesa)
                .HasPrincipalKey(prop => prop.CodDespesa);
        }
    }
}