using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewTecnicoDeslocamentoMap : IEntityTypeConfiguration<ViewTecnicoDeslocamento>
    {
        public void Configure(EntityTypeBuilder<ViewTecnicoDeslocamento> builder)
        {
            builder
                .ToView("vwc_v2_tecnico_deslocamento")
                .HasNoKey();
        }
    }
}