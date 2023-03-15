using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class InstalacaoMotivoMultaMap : IEntityTypeConfiguration<InstalacaoMotivoMulta>
    {
        public void Configure(EntityTypeBuilder<InstalacaoMotivoMulta> builder)
        {
            builder.ToTable("InstalMotivoMulta");

            builder
                .HasKey(i => new { i.CodInstalMotivoMulta });
        }
    }
}