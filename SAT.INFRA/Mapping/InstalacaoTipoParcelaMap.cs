using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class InstalacaoTipoParcelaMap : IEntityTypeConfiguration<InstalacaoTipoParcela>
    {
        public void Configure(EntityTypeBuilder<InstalacaoTipoParcela> builder)
        {
            builder.ToTable("InstalTipoParcela");

            builder
                .HasKey(i => new { i.CodInstalTipoParcela });
        }
    }
}