using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class LaudoStatusMap : IEntityTypeConfiguration<LaudoStatus>
    {
        public void Configure(EntityTypeBuilder<LaudoStatus> builder)
        {
            builder
                .ToTable("LaudoStatus");

            builder
                .HasKey(prop => prop.CodLaudoStatus);
        }
    }
}