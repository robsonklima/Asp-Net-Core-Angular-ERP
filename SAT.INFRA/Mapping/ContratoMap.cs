using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ContratoMap : IEntityTypeConfiguration<Contrato>
    {
        public void Configure(EntityTypeBuilder<Contrato> builder)
        {
            builder
                .ToTable("Contrato");

            builder
                .HasKey(prop => prop.CodContrato);

            builder
                .HasOne(prop => prop.Cliente)
                .WithMany()
                .HasForeignKey(prop => prop.CodCliente)
                .HasPrincipalKey(prop => prop.CodCliente);

            builder
                .HasOne(prop => prop.TipoContrato)
                .WithMany()
                .HasForeignKey(prop => prop.CodTipoContrato)
                .HasPrincipalKey(prop => prop.CodTipoContrato);

            builder
                .HasMany(prop => prop.Lotes)
                .WithOne()
                .HasForeignKey(prop => prop.CodContrato)
                .HasPrincipalKey(prop => prop.CodContrato);

            builder
               .HasMany(prop => prop.ContratoServico)
               .WithOne()
               .HasForeignKey(prop => prop.CodContrato)
               .HasPrincipalKey(prop => prop.CodContrato);

            builder
                .Ignore(prop => prop.ContratoEquipamento);
        }
    }
}
