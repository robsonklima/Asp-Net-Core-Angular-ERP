using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class PontoPeriodoUsuarioMap : IEntityTypeConfiguration<PontoPeriodoUsuario>
    {
        public void Configure(EntityTypeBuilder<PontoPeriodoUsuario> builder)
        {
            builder
                .ToTable("PontoPeriodoUsuario");

            builder
                .Property(p => p.CodUsuario)
                .HasConversion(v => v.ToString(),
                               v => v.ToLower());
        }
    }
}