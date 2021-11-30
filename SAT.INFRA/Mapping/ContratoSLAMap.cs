using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ContratoSLAMap : IEntityTypeConfiguration<ContratoSLA>
    {
        public void Configure(EntityTypeBuilder<ContratoSLA> builder)
        {
            builder
                .ToTable("ContratoSLA");

            builder
                .HasKey(prop => new { prop.CodContrato, prop.CodSLA });
        }
    }
}
