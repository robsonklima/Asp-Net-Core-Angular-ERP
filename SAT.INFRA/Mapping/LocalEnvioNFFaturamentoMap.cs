using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class LocalEnvioNFFaturamentoMap : IEntityTypeConfiguration<LocalEnvioNFFaturamento>
    {
        public void Configure(EntityTypeBuilder<LocalEnvioNFFaturamento> builder)
        {
            builder.ToTable("LocalEnvioNFFaturamento");
            builder.HasKey(prop => prop.CodLocalEnvioNFFaturamento);
        }
    }
}