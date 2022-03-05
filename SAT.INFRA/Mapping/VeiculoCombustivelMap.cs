using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class VeiculoCombustivelMap : IEntityTypeConfiguration<VeiculoCombustivel>
    {
        public void Configure(EntityTypeBuilder<VeiculoCombustivel> builder)
        {
            builder.ToTable("VeiculoCombustivel");
            builder.HasKey(prop => prop.CodVeiculoCombustivel);
        }
    }
}