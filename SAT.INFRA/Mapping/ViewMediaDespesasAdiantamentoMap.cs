using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewMediaDespesasAdiantamentoMap : IEntityTypeConfiguration<ViewMediaDespesasAdiantamento>
    {
        public void Configure(EntityTypeBuilder<ViewMediaDespesasAdiantamento> builder)
        {
            builder
                .ToView("vwc_ConsAdiantMediaDespesas")
                .HasNoKey();
        }
    }
}