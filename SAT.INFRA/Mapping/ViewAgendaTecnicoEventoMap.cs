using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Views;

namespace SAT.INFRA.Mapping
{
    public class ViewAgendaTecnicoEventoMap : IEntityTypeConfiguration<ViewAgendaTecnicoEvento>
    {
        public void Configure(EntityTypeBuilder<ViewAgendaTecnicoEvento> builder)
        {
            builder
                .ToView("vwc_v2_agendatecnico")
                .HasNoKey();

            builder.Ignore(agenda => agenda.Titulo);
            builder.Ignore(agenda => agenda.Editavel);
            builder.Ignore(agenda => agenda.Clientes);
        }
    }
}