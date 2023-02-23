using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class InstalacaoStatusMap : IEntityTypeConfiguration<InstalacaoStatus>
    {
        public void Configure(EntityTypeBuilder<InstalacaoStatus> builder)
        {
            builder.ToTable("InstalacaoStatus");

            builder
                .HasKey(i => new { i.CodInstalStatus });                                                             
        }
    }
}