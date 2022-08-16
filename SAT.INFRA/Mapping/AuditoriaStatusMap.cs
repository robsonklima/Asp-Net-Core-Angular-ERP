using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping {
    public class AuditoriaStatusMap : IEntityTypeConfiguration<AuditoriaStatus>
    {
        public void Configure(EntityTypeBuilder<AuditoriaStatus> builder)
        {
            builder.
                ToTable("AuditoriaStatus");
            
            builder.
                HasKey(i => new { i.CodAuditoriaStatus });

        }
    }
}