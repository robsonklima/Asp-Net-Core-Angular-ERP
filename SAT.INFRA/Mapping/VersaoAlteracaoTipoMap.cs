using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class VersaoAlteracaoTipoMap : IEntityTypeConfiguration<VersaoAlteracaoTipo>
    {
        public void Configure(EntityTypeBuilder<VersaoAlteracaoTipo> builder)
        {
            builder.ToTable("SatVersaoAlteracaoTipo");
            builder.HasKey(prop => prop.CodSatVersaoAlteracaoTipo);
        }
    }
}