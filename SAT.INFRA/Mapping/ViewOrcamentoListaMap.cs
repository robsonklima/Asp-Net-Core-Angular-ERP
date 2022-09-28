using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewOrcamentoListaMap : IEntityTypeConfiguration<ViewOrcamentoLista>
    {
        public void Configure(EntityTypeBuilder<ViewOrcamentoLista> builder)
        {
            builder
                .ToView("vwc_v2_orcamento_lista")
                .HasNoKey();
        }
    }
}