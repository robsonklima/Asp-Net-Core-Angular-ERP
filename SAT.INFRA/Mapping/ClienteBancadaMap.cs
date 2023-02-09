using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ClienteBancadaMap : IEntityTypeConfiguration<ClienteBancada>
    {
        public void Configure(EntityTypeBuilder<ClienteBancada> builder)
        {
            builder
                .ToTable("ClienteBancada");

            builder
                .HasKey(i => i.CodClienteBancada);

            builder
                .HasOne(prop => prop.Cidade)
                .WithMany()
                .HasForeignKey(prop => prop.CodCidade)
                .HasPrincipalKey(prop => prop.CodCidade);

        }
    }
}