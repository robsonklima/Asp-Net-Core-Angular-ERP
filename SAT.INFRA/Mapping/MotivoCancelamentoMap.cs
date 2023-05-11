using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class MotivoCancelamentoMap : IEntityTypeConfiguration<MotivoCancelamento>
    {
        public void Configure(EntityTypeBuilder<MotivoCancelamento> builder)
        {
            builder.ToTable("MotivoCancelamento");
            builder.HasKey(i => i.CodMotivoCancelamento);
            builder.Property(i => i.Motivo).HasColumnName("MotivoCancelamento");
        }
    }
}