using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class PecasLaboratorioMap : IEntityTypeConfiguration<PecasLaboratorio>
    {
        public void Configure(EntityTypeBuilder<PecasLaboratorio> builder)
        {
            builder
                .ToTable("PecasLaboratorio")
                .HasNoKey();

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
                .HasOne(i => i.Peca)
                .WithMany()
                .HasForeignKey(i => i.CodPeca)
                .HasPrincipalKey(i => i.CodPeca);

            builder
                .HasOne(i => i.ORCheckList)
                .WithMany()
                .HasForeignKey(i => i.CodChecklist)
                .HasPrincipalKey(i => i.CodORCheckList);
        }
    }
}