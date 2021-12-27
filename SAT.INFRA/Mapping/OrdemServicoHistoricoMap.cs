using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrdemServicoHistoricoMap : IEntityTypeConfiguration<OrdemServicoHistorico>
    {
        public void Configure(EntityTypeBuilder<OrdemServicoHistorico> builder)
        {
            builder.ToTable("HistOS");
            builder.HasKey(prop => prop.CodHistOS);
        }
    }
}