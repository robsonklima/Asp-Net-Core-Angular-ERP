using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class IntencaoMap : IEntityTypeConfiguration<Intencao>
    {
        public void Configure(EntityTypeBuilder<Intencao> builder)
        {
            builder.ToTable("Intencao");
            builder.HasKey(prop => prop.CodIntencao);
        }
    }
}