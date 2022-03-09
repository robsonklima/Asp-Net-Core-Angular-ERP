using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class PecaListaMap : IEntityTypeConfiguration<PecaLista>
    {
        public void Configure(EntityTypeBuilder<PecaLista> builder)
        {
            builder
                .ToTable("PecaLista");

            builder
                .HasKey(i => i.CodPecaLista);
        }
    }
}