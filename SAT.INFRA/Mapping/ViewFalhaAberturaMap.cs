using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewFalhaAberturaMap : IEntityTypeConfiguration<ViewFalhaAbertura>
    {
        public void Configure(EntityTypeBuilder<ViewFalhaAbertura> builder)
        {
            builder
                .ToView("vwc_V2_FalhaAbertura")
                .HasNoKey();
        }
    }
}