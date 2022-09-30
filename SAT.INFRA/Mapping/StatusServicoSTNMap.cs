using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class StatusServicoSTNMap : IEntityTypeConfiguration<StatusServicoSTN>
    {
        public void Configure(EntityTypeBuilder<StatusServicoSTN> builder)
        {
            builder
                .ToTable("StatusServicoSTN");

            builder
                .HasKey(prop => prop.CodStatusServicoSTN);
        }
    }
}