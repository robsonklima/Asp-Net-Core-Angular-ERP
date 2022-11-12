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
                .Property(i => i.CodDespesaCartaoCombustivel)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(i => i.TicketLogUsuarioCartaoPlaca)
                .WithOne()
                .HasForeignKey<DespesaCartaoCombustivel>(i => i.Numero)
                .HasPrincipalKey<TicketLogUsuarioCartaoPlaca>(i => i.NumeroCartao);

            builder
                .HasMany(prop => prop.Transacoes)
                .WithOne()
                .HasForeignKey(prop => prop.NumeroCartao)
                .HasPrincipalKey(prop => prop.Numero);
        }
    }
}