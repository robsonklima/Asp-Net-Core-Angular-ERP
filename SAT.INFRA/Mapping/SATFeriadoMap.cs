using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class SATFeriadoMap : IEntityTypeConfiguration<SATFeriado>
    {
        public void Configure(EntityTypeBuilder<SATFeriado> builder)
        {
            builder.ToTable("SATFeriados");
            builder.HasKey(i => i.CodSATFeriado);
        }
    }
}