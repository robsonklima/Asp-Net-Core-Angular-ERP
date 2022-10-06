using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ProtocoloSTNMap : IEntityTypeConfiguration<ProtocoloSTN>
    {
        public void Configure(EntityTypeBuilder<ProtocoloSTN> builder)
        {
            builder.ToTable("ProtocoloSTN");
            builder.HasKey(prop => prop.CodProtocoloSTN);
        }
    }
}