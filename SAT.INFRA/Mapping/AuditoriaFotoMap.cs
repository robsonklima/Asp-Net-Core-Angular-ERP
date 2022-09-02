using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping {
    public class AuditoriaFotoMap : IEntityTypeConfiguration<AuditoriaFoto>
    {
        public void Configure(EntityTypeBuilder<AuditoriaFoto> builder)
        {
            builder.ToTable("AuditoriaFoto");
            builder.HasKey(i => new { i.CodAuditoriaFoto });
        }
    }
}