using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OSBancadaMap : IEntityTypeConfiguration<OSBancada>
    {
        public void Configure(EntityTypeBuilder<OSBancada> builder)
        {
            builder
                .ToTable("OSbancada");

            builder
                .HasKey(i => i.CodOsbancada);

            builder
                .HasOne(prop => prop.Filial)
                .WithMany()
                .HasForeignKey(prop => prop.CodFilial)
                .HasPrincipalKey(prop => prop.CodFilial);
            
            builder
                .HasOne(prop => prop.ClienteBancada)
                .WithMany()
                .HasForeignKey(prop => prop.CodClienteBancada)
                .HasPrincipalKey(prop => prop.CodClienteBancada);

        }
    }
}