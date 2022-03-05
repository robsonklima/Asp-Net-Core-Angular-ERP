using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class TecnicoVeiculoMap : IEntityTypeConfiguration<TecnicoVeiculo>
    {
        public void Configure(EntityTypeBuilder<TecnicoVeiculo> builder)
        {
            builder
                .ToTable("TecnicoVeiculo");

            builder
                .HasKey(prop => prop.CodTecnicoVeiculo);

            builder
                .HasOne(i => i.Combustivel)
                .WithMany()
                .HasForeignKey("CodVeiculoCombustivel")
                .HasPrincipalKey("CodVeiculoCombustivel");
        }
    }
}