using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OSPrazoAtendimentoMap : IEntityTypeConfiguration<OSPrazoAtendimento>
    {
        public void Configure(EntityTypeBuilder<OSPrazoAtendimento> builder)
        {
            builder
                .ToTable("OSPrazoAtendimento");

            builder
                .HasKey(i => i.CodOSPrazoAtendimento);
        }
    }
}