using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ORTempoReparoMap : IEntityTypeConfiguration<ORTempoReparo>
    {
        public void Configure(EntityTypeBuilder<ORTempoReparo> builder)
        {
            builder.ToTable("ORTempoReparo");
            builder.HasKey(i => i.CodORTempoReparo);
        }
    }
}