using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ContratoServicoMap : IEntityTypeConfiguration<ContratoServico>
    {
        public void Configure(EntityTypeBuilder<ContratoServico> builder)
        {
            builder
                .ToTable("ContratoServico");

            builder
                .HasKey(i => i.CodContratoServico);

            builder
                .HasOne(i => i.TipoServico)
                .WithMany()
                .HasForeignKey(i => i.CodServico)
                .HasPrincipalKey(i => i.CodServico);
        }
    }
}