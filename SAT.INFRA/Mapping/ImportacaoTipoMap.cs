using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ImportacaoTipoMap : IEntityTypeConfiguration<ImportacaoTipo>
    {
        public void Configure(EntityTypeBuilder<ImportacaoTipo> builder)
        {
            builder.ToTable("ImportacaoTipo");

            builder
                .HasKey(i => i.CodImportacaoTipo);

        }         
    }
}
