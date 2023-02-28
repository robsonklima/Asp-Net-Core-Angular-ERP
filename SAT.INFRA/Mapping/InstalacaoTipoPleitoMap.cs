using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class InstalacaoTipoPleitoMap : IEntityTypeConfiguration<InstalacaoTipoPleito>
    {
        public void Configure(EntityTypeBuilder<InstalacaoTipoPleito> builder)
        {
            builder.ToTable("InstalTipoPleito");
            builder.HasKey(i => new { i.CodInstalTipoPleito });                                   
        }
    }
}