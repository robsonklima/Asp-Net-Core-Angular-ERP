using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ORDefeitoMap : IEntityTypeConfiguration<ORDefeito>
    {
        public void Configure(EntityTypeBuilder<ORDefeito> builder)
        {
            builder.ToTable("ORDefeito");
            builder.HasKey(i => i.CodDefeito);

            builder.Property(i => i.Descricao).HasColumnName("Desricao");

        }
    }
}