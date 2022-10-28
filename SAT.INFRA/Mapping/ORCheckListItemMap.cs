using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ORCheckListItemMap : IEntityTypeConfiguration<ORCheckListItem>
    {
        public void Configure(EntityTypeBuilder<ORCheckListItem> builder)
        {
            builder.ToTable("ORCheckListItem");
            builder.HasKey(i => i.CodORCheckListItem);

            builder
                .HasOne(prop => prop.Peca)
                .WithMany()
                .HasForeignKey(prop => prop.CodMagnus)
                .HasPrincipalKey(prop => prop.CodMagnus);
        }
    }
}