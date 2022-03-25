using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class FeriadoMap : IEntityTypeConfiguration<Feriado>
    {
        public void Configure(EntityTypeBuilder<Feriado> builder)
        {
            builder
                .ToTable("Feriados");

            builder
                .HasKey(i => i.CodFeriado);
        }
    }
}