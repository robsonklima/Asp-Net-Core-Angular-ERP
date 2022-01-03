using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class EnderecoFaturamentoVinculadoMap : IEntityTypeConfiguration<EnderecoFaturamentoVinculado>
    {
        public void Configure(EntityTypeBuilder<EnderecoFaturamentoVinculado> builder)
        {
            builder
                .ToTable("LocalEnvioNFFaturamentoVinculado");

            builder
                .HasNoKey();
        }
    }
}