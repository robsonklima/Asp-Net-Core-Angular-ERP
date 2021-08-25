using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class NavegacaoConfiguracaoMap : IEntityTypeConfiguration<NavegacaoConfiguracao>
    {
        public void Configure(EntityTypeBuilder<NavegacaoConfiguracao> builder)
        {
            builder.ToTable("NavegacaoConfiguracao");

            builder.HasKey(prop => prop.CodNavegacao);
            builder.HasKey(prop => prop.CodPerfil);
            builder.HasOne(prop => prop.Perfil);
        }
    }
}
