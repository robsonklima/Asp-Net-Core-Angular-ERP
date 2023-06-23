using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewIntegracaoBBMap : IEntityTypeConfiguration<ViewIntegracaoBB>
    {
        public void Configure(EntityTypeBuilder<ViewIntegracaoBB> builder)
        {
            builder
                .ToView("vwc_IntegracaoBB")
                .HasNoKey();
        }
    }
}