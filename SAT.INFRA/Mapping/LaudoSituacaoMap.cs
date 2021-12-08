using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class LaudoSituacaoMap : IEntityTypeConfiguration<LaudoSituacao>
    {
        public void Configure(EntityTypeBuilder<LaudoSituacao> builder)
        {
            builder
                .ToTable("LaudoSituacao");

            builder
                .HasKey(prop => prop.CodLaudoSituacao);
        }
    }
}