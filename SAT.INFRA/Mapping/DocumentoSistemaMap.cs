using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class DocumentoSistemaMap : IEntityTypeConfiguration<DocumentoSistema>
    {
        public void Configure(EntityTypeBuilder<DocumentoSistema> builder)
        {
            builder.ToTable("DocumentoSistema");
            builder.HasKey(prop => prop.CodDocumentoSistema);
        }
    }
}