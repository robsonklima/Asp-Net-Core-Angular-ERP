using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class InstalacaoViewMap : IEntityTypeConfiguration<InstalacaoView>
    {
        public void Configure(EntityTypeBuilder<InstalacaoView> builder)
        {
            builder.ToTable("vwc_v2_instalacao");
            builder.HasNoKey();
        }
    }
}