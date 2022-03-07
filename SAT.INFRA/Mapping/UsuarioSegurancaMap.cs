using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class UsuarioSegurancaMap : IEntityTypeConfiguration<UsuarioSeguranca>
    {
        public void Configure(EntityTypeBuilder<UsuarioSeguranca> builder)
        {
            builder.ToTable("UsuarioSeguranca");
            builder.HasKey(prop => prop.CodUsuario);
        }

    }
}