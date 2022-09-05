using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ArquivoBanrisulMap : IEntityTypeConfiguration<ArquivoBanrisul>
    {
        public void Configure(EntityTypeBuilder<ArquivoBanrisul> builder)
        {
            builder.ToTable("GerenciaArquivosBanrisul");
            builder.HasKey(i => new { i.CodGerenciaArquivosBanrisul });
        }
    }
}