using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class LocalEnvioNFFaturamentoVinculadoMap : IEntityTypeConfiguration<LocalEnvioNFFaturamentoVinculado>
    {
        public void Configure(EntityTypeBuilder<LocalEnvioNFFaturamentoVinculado> builder)
        {
            builder.ToTable("LocalEnvioNFFaturamentoVinculado");
            
            builder.HasKey(prop => prop.CodLocalEnvioNFFaturamento);
            builder.HasKey(prop => prop.CodPosto);
            builder.HasKey(prop => prop.CodContrato);
        }
    }
}