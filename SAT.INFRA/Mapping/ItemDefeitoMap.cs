using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ItemDefeitoMap : IEntityTypeConfiguration<ItemDefeito>
    {
        public void Configure(EntityTypeBuilder<ItemDefeito> builder)
        {
            builder.ToTable("ItemDefeito");
            builder.HasKey(i => i.CodItemDefeito);

            builder
                .HasOne(prop => prop.ORItem)
                .WithMany()
                .HasForeignKey(prop => prop.CodORItem)
                .HasPrincipalKey(prop => prop.CodORItem);

            builder
                .HasOne(prop => prop.Usuario)
                .WithMany()
                .HasForeignKey(prop => prop.CodTecnico)
                .HasPrincipalKey(prop => prop.CodUsuario);

            builder
                .HasOne(prop => prop.ORDefeito)
                .WithMany()
                .HasForeignKey(prop => prop.CodDefeito)
                .HasPrincipalKey(prop => prop.CodDefeito);

        }
    }
}