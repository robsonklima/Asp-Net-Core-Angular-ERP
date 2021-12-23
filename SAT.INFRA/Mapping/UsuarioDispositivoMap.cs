using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class UsuarioDispositivoMap : IEntityTypeConfiguration<UsuarioDispositivo>
    {
        public void Configure(EntityTypeBuilder<UsuarioDispositivo> builder)
        {
            builder.ToTable("UsuarioDispositivo");
            builder.HasKey(prop => prop.CodUsuarioDispositivo);
        }
    }
}