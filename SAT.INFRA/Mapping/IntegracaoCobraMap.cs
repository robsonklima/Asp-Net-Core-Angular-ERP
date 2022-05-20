using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class IntegracaoCobraMap : IEntityTypeConfiguration<IntegracaoCobra>
    {
        public void Configure(EntityTypeBuilder<IntegracaoCobra> builder)
        {
            builder.ToTable("IntegracaoCobra");
            builder.HasKey(prop => new { prop.CodOS, prop.NumOSCliente });
        }
    }
}
