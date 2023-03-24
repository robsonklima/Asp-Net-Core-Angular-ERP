using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class RelatorioAtendimentoPecaStatusMap : IEntityTypeConfiguration<RelatorioAtendimentoPecaStatus>
    {
        public void Configure(EntityTypeBuilder<RelatorioAtendimentoPecaStatus> builder)
        {
            builder.
                ToTable("RATPecasStatus");

            builder.
                HasKey(i => new { i.CodRatpecasStatus });
        }
    }
}