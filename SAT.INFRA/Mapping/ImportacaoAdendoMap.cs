using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ImportacaoAdendoMap : IEntityTypeConfiguration<ImportacaoAdendo>
    {
        public void Configure(EntityTypeBuilder<ImportacaoAdendo> builder)
        {
            builder.ToTable("ImportacaoAdendo");
            builder.HasKey(i => i.CodAdendo);
        }
    }
}