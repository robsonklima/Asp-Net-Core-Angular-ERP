using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class PecaMap : IEntityTypeConfiguration<Peca>
    {
        public void Configure(EntityTypeBuilder<Peca> builder)
        {
            builder
                .ToTable("Peca");

            builder
                .HasKey(i => i.CodPeca);

            builder
                .HasOne(i => i.PecaFamilia)
                .WithMany()
                .HasForeignKey(i => i.CodPecaFamilia)
                .HasPrincipalKey(i => i.CodPecaFamilia);

            builder
               .HasOne(i => i.PecaStatus)
               .WithMany()
               .HasForeignKey(i => i.CodPecaStatus)
               .HasPrincipalKey(i => i.CodPecaStatus);

            builder
                .Property(i => i.DataHoraAtualizacaoValor)
                .HasComputedColumnSql("DataHoraAtualizacaoValor");

            builder
               .HasMany<ClientePeca>(i => i.ClientePeca)
               .WithOne()
               .HasForeignKey(i => i.CodPeca)
               .HasPrincipalKey(i => i.CodPeca);

            builder
               .HasOne<ClientePecaGenerica>(i => i.ClientePecaGenerica)
               .WithOne()
               .HasForeignKey<ClientePecaGenerica>(i => i.CodPeca)
               .HasPrincipalKey<Peca>(i => i.CodPeca);
        }
    }
}