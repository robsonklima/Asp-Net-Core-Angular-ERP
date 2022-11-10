using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ItemSolucaoMap : IEntityTypeConfiguration<ItemSolucao>
    {
        public void Configure(EntityTypeBuilder<ItemSolucao> builder)
        {
            builder.ToTable("ItemSolucao");
            builder.HasKey(i => i.CodItemSolucao);

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
                .HasOne(prop => prop.ORSolucao)
                .WithMany()
                .HasForeignKey(prop => prop.CodSolucao)
                .HasPrincipalKey(prop => prop.CodSolucao);

        }
    }
}