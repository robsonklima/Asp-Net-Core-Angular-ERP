using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ORStatusMap : IEntityTypeConfiguration<ORStatus>
    {
        public void Configure(EntityTypeBuilder<ORStatus> builder)
        {
            builder.ToTable("ORStatus");
            builder.HasKey(i => i.CodStatus);
        }
    }
}