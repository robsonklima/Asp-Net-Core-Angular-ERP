using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class AcaoMap : IEntityTypeConfiguration<Acao>
    {
        public void Configure(EntityTypeBuilder<Acao> builder)
        {
            builder
                .ToTable("Acao");

            builder
                .HasKey(i => i.CodAcao);
        }
    }
}