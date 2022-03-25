using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class LiderTecnicoMap : IEntityTypeConfiguration<LiderTecnico>
    {
        public void Configure(EntityTypeBuilder<LiderTecnico> builder)
        {
            builder
                .ToTable("LiderTecnico");

            builder
                .HasKey(i => i.CodLiderTecnico);
        }
    }
}