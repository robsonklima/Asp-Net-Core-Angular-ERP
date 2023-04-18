using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class AdiantamentoRDsPendentesViewMap : IEntityTypeConfiguration<AdiantamentoRDsPendentesView>
    {
        public void Configure(EntityTypeBuilder<AdiantamentoRDsPendentesView> builder)
        {
            builder
                .ToView("vwc_ConsAdiantRDsPendentes")
                .HasNoKey();
        }
    }
}