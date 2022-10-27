using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ORDestinoMap : IEntityTypeConfiguration<ORDestino>
    {
        public void Configure(EntityTypeBuilder<ORDestino> builder)
        {
            builder.ToTable("ORDestino");
            builder.HasKey(i => i.CodDestino);
        }
    }
}