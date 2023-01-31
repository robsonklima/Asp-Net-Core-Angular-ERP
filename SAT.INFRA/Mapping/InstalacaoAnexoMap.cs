using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class InstalacaoAnexoMap : IEntityTypeConfiguration<InstalacaoAnexo>
    {
        public void Configure(EntityTypeBuilder<InstalacaoAnexo> builder)
        {
            builder
                .ToTable("InstalAnexo");

            builder
                .HasKey(i => i.CodInstalAnexo);

            builder
                .Ignore(i => i.Base64);
        }
    }
}
