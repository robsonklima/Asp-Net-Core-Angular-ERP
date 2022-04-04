using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Mapping
{
    public class ViewTecnicoTempoAtendimentoMap : IEntityTypeConfiguration<ViewTecnicoTempoAtendimento>
    {
        public void Configure(EntityTypeBuilder<ViewTecnicoTempoAtendimento> builder)
        {
            builder
                .ToView("vwc_v2_tecnico_tempo_atendimento")
                .HasNoKey();
        }
    }
}