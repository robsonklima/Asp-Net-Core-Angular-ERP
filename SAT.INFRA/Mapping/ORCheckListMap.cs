using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ORCheckListMap : IEntityTypeConfiguration<ORCheckList>
    {
        public void Configure(EntityTypeBuilder<ORCheckList> builder)
        {
            builder.ToTable("ORCheckList");
            builder.HasKey(i => i.CodORChecklist);
        }
    }
}