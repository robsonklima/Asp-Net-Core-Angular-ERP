using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class MRPLogixMap : IEntityTypeConfiguration<MRPLogix>
    {
        public void Configure(EntityTypeBuilder<MRPLogix> builder)
        {
            builder.ToTable("MRPLogix");
            builder.HasKey(i => i.CodMRPLogix);
        }
    }
}