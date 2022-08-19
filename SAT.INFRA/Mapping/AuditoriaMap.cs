using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class AuditoriaMap : IEntityTypeConfiguration<Auditoria>
    {
        public void Configure(EntityTypeBuilder<Auditoria> builder)
        {
            builder.
                ToTable("Auditoria");

            builder.
                HasKey(i => new { i.CodAuditoria });

            builder
                .HasOne(prop => prop.Usuario)
                .WithMany()
                .HasForeignKey(prop => prop.CodUsuario)
                .HasPrincipalKey(prop => prop.CodUsuario);

            builder
                .HasOne(prop => prop.AuditoriaStatus)
                .WithMany()
                .HasForeignKey(prop => prop.CodAuditoriaStatus)
                .HasPrincipalKey(prop => prop.CodAuditoriaStatus);

            builder
                .HasOne(prop => prop.AuditoriaVeiculo)
                .WithMany()
                .HasForeignKey(prop => prop.CodAuditoriaVeiculo)
                .HasPrincipalKey(prop => prop.CodAuditoriaVeiculo);

            builder.Ignore(prop => prop.QtdDespesasPendentes);
        }
    }
}