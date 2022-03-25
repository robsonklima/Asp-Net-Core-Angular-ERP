using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class BancadaListaMap : IEntityTypeConfiguration<BancadaLista>
    {
        public void Configure(EntityTypeBuilder<BancadaLista> builder)
        {
            builder
                .ToTable("BancadaLista");

            builder
                .HasKey(i => i.CodBancadaLista);
        }
    }
}