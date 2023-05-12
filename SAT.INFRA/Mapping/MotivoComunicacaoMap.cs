using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class MotivoComunicacaoMap : IEntityTypeConfiguration<MotivoComunicacao>
    {
        public void Configure(EntityTypeBuilder<MotivoComunicacao> builder)
        {
            builder.ToTable("MotivoComunicacao");
            builder.HasKey(i => i.CodMotivoComunicacao);
            builder.Property(p => p.Motivo).HasColumnName("MotivoComunicacao");

        }
    }
}