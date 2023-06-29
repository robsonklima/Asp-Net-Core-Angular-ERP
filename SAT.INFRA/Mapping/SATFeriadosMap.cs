using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class SATFeriadosMap : IEntityTypeConfiguration<SATFeriados>
    {
        public void Configure(EntityTypeBuilder<SATFeriados> builder)
        {
            builder.ToTable("SATFeriados");
            builder.HasKey(i => i.CodSATFeriados);
        }
    }
}