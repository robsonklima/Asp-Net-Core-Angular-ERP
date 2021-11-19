using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class DespesaCartaoCombustivelMap : IEntityTypeConfiguration<DespesaCartaoCombustivel>
    {
        public void Configure(EntityTypeBuilder<DespesaCartaoCombustivel> builder)
        {
            builder
                .ToTable("DespesaCartaoCombustivel");

            builder
                .HasKey(prop => prop.CodDespesaCartaoCombustivel);

            builder
                .HasOne(i => i.TicketLogUsuarioCartaoPlaca)
                .WithOne()
                .HasForeignKey<DespesaCartaoCombustivel>(i => i.Numero)
                .HasPrincipalKey<TicketLogUsuarioCartaoPlaca>(i => i.NumeroCartao);
        }
    }
}