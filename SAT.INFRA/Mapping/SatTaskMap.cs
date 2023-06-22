using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class SatTaskMap : IEntityTypeConfiguration<SatTask>
    {
        public void Configure(EntityTypeBuilder<SatTask> builder)
        {
            builder.ToTable("SatTask");
            builder.HasKey(i => i.CodSatTask);
            builder
                .HasOne(i => i.Tipo)
                .WithMany()
                .HasForeignKey(prop => prop.CodSatTaskTipo)
                .HasPrincipalKey(prop => prop.CodSatTaskTipo);
        }
    }
}