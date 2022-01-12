using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrcamentoISSMap : IEntityTypeConfiguration<OrcamentoISS>
    {
        public void Configure(EntityTypeBuilder<OrcamentoISS> builder)
        {
            builder
                .ToTable("OrcISS");

            builder
                .HasKey(prop => prop.CodOrcIss);
        }
    }
}