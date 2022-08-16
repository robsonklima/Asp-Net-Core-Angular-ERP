using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping {
    public class AuditoriaFotoMap : IEntityTypeConfiguration<Auditoria>
    {
        public void Configure(EntityTypeBuilder<Auditoria> builder)
        {
            builder.
                ToTable("AuditoriaFoto");
            
            builder.
                HasKey(i => new { i.CodAuditoria });

        }
    }
}