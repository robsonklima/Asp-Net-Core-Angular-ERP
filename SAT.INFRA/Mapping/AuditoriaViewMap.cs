using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class AuditoriaViewMap : IEntityTypeConfiguration<AuditoriaView>
    {
        public void Configure(EntityTypeBuilder<AuditoriaView> builder)
        {
            builder.ToTable("vwc_v2_auditorias");
            builder.HasNoKey();
        }
    }
}