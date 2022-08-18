using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class DespesaConfiguracaoCombustivelMap : IEntityTypeConfiguration<DespesaConfiguracaoCombustivel>
    {
        public void Configure(EntityTypeBuilder<DespesaConfiguracaoCombustivel> builder)
        {
            builder
                .ToTable("DespesaConfiguracaoCombustivel");

            builder
                .HasKey(i => i.CodDespesaConfiguracaoCombustivel);
        }
    }
}