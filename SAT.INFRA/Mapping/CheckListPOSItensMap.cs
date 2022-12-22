using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class CheckListPOSItensMap : IEntityTypeConfiguration<CheckListPOSItens>
    {
        public void Configure(EntityTypeBuilder<CheckListPOSItens> builder)
        {
            builder.
                ToTable("CheckListPOSItens");

            builder.
                HasKey(i => new { i.CodCheckListPOSItens });
        }
    }
}