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
                .HasOne(i => i.Cidade)
                .WithMany()
                .HasForeignKey("CodCidade")
                .HasPrincipalKey("CodCidade");
        }
    }
}