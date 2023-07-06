using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class NavegacaoConfiguracaoTipoMap : IEntityTypeConfiguration<NavegacaoConfiguracaoTipo>
    {
        public void Configure(EntityTypeBuilder<NavegacaoConfiguracaoTipo> builder)
        {
            builder.ToTable("NavegacaoConfTipo");
            builder.HasKey(prop => prop.CodNavegacaoConfTipo);
        }
    }
}