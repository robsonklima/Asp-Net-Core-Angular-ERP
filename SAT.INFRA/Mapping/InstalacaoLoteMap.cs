using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
public class InstalacaoLoteMap : IEntityTypeConfiguration<InstalacaoLote>
    {
        public void Configure(EntityTypeBuilder<InstalacaoLote> builder)
        {
            builder.ToTable("InstalLote");

            builder
                .HasKey(i => new { i.CodInstalLote });                                                                 
        }
    }
}