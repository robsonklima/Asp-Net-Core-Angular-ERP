using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrdemServicoHistoricoMap : IEntityTypeConfiguration<OrdemServicoHistorico>
    {
        public void Configure(EntityTypeBuilder<OrdemServicoHistorico> builder)
        {
            builder.ToTable("HistOS");
            builder.HasKey(prop => prop.CodHistOS);

            builder
                .HasOne(prop => prop.StatusServico)
                .WithMany()
                .HasForeignKey(prop => prop.CodStatusServico)
                .HasPrincipalKey(prop => prop.CodStatusServico);

            builder
                .HasOne(prop => prop.TipoIntervencao)
                .WithMany()
                .HasForeignKey(prop => prop.CodTipoIntervencao)
                .HasPrincipalKey(prop => prop.CodTipoIntervencao);

            builder
                .HasOne(prop => prop.Cliente)
                .WithMany()
                .HasForeignKey(prop => prop.CodCliente)
                .HasPrincipalKey(prop => prop.CodCliente);

            builder
                .HasOne(prop => prop.LocalAtendimento)
                .WithMany()
                .HasForeignKey(prop => prop.CodPosto)
                .HasPrincipalKey(prop => prop.CodPosto);

            builder
                .HasOne(prop => prop.EquipamentoContrato)
                .WithMany()
                .HasForeignKey(prop => prop.CodEquipContrato)
                .HasPrincipalKey(prop => prop.CodEquipContrato);

            builder
                .HasOne(prop => prop.Autorizada)
                .WithMany()
                .HasForeignKey(prop => prop.CodAutorizada)
                .HasPrincipalKey(prop => prop.CodAutorizada);

            builder
                .HasOne(prop => prop.Tecnico)
                .WithMany()
                .HasForeignKey(prop => prop.CodTecnico)
                .HasPrincipalKey(prop => prop.CodTecnico);

            builder
                .HasOne(prop => prop.Usuario)
                .WithMany()
                .HasForeignKey(prop => prop.CodUsuarioManutencao)
                .HasPrincipalKey(prop => prop.CodUsuario);
        }
    }
}