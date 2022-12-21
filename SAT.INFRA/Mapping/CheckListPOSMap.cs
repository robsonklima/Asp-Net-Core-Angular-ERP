using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class CheckListPOSMap : IEntityTypeConfiguration<CheckListPOS>
    {
        public void Configure(EntityTypeBuilder<CheckListPOS> builder)
        {
            builder.
                ToTable("CheckListPOS");

            builder.
                HasKey(i => new { i.CodCheckListPOS });

            builder
                .HasOne(prop => prop.CheckListPOSItens)
                .WithMany()
                .HasForeignKey(prop => prop.CodCheckListPOSItens)
                .HasPrincipalKey(prop => prop.CodCheckListPOSItens);

        }
    }
}