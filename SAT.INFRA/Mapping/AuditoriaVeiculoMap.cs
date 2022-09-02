using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class AuditoriaVeiculoMap : IEntityTypeConfiguration<AuditoriaVeiculo>
    {
        public void Configure(EntityTypeBuilder<AuditoriaVeiculo> builder)
        {
            builder
                .ToTable("AuditoriaVeiculo");

            builder
                .HasKey(i => i.CodAuditoriaVeiculo);

            builder
                .HasMany(prop => prop.Acessorios)
                .WithOne()
                .HasForeignKey(prop => prop.CodAuditoriaVeiculo)
                .HasPrincipalKey(prop => prop.CodAuditoriaVeiculo);
                            
            builder
                .HasOne(prop => prop.AuditoriaVeiculoTanque)
                .WithMany()
                .HasForeignKey(prop => prop.CodAuditoriaVeiculoTanque)
                .HasPrincipalKey(prop => prop.CodAuditoriaVeiculoTanque);

        }
    }
}