using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ItemXORCheckListMap : IEntityTypeConfiguration<ItemXORCheckList>
    {
        public void Configure(EntityTypeBuilder<ItemXORCheckList> builder)
        {
            builder.ToTable("ItemXORCheckList");
            builder.HasKey(i => i.CodItemChecklist);

            builder
                .HasOne(prop => prop.ORItem)
                .WithMany()
                .HasForeignKey(prop => prop.CodORItem)
                .HasPrincipalKey(prop => prop.CodORItem);

            builder
                .HasOne(prop => prop.ORCheckList)
                .WithMany()
                .HasForeignKey(prop => prop.CodORCheckList)
                .HasPrincipalKey(prop => prop.CodORCheckList);
        }
    }
}