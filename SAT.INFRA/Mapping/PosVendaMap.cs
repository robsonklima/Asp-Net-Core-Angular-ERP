using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class PosVendaMap : IEntityTypeConfiguration<PosVenda>
    {
        public void Configure(EntityTypeBuilder<PosVenda> builder)
        {
            builder.ToTable("PosVenda");
            builder.HasKey(prop => prop.CodPosVenda);
        }
    }
}