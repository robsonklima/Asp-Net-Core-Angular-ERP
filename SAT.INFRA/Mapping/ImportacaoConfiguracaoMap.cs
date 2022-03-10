using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ImportacaoConfiguracaoMap : IEntityTypeConfiguration<ImportacaoConfiguracao>
    {
        public void Configure(EntityTypeBuilder<ImportacaoConfiguracao> builder)
        {
            builder.ToTable("ImportacaoConf");

            builder
                .HasKey(i => i.CodImportacaoConf);

            builder
                .HasOne(i => i.ImportacaoTipo)
                .WithOne()
                .HasForeignKey<ImportacaoTipo>(i => i.CodImportacaoTipo)
                .HasPrincipalKey<ImportacaoConfiguracao>(i => i.CodImportacaoTipo);
        }         
    }
}
