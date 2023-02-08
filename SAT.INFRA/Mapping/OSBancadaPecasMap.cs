using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OSBancadaPecasMap : IEntityTypeConfiguration<OSBancadaPecas>
    {
        public void Configure(EntityTypeBuilder<OSBancadaPecas> builder)
        {
            builder
                .ToTable("OSBancadaPecas");

            builder
                .HasKey(c => new { c.CodOsbancada, c.CodPecaRe5114 });
                

            builder
                .HasOne(prop => prop.OSBancada)
                .WithMany()
                .HasForeignKey(prop => prop.CodOsbancada)
                .HasPrincipalKey(prop => prop.CodOsbancada);
            
            builder
                .HasOne(prop => prop.PecaRE5114)
                .WithMany()
                .HasForeignKey(prop => prop.CodPecaRe5114)
                .HasPrincipalKey(prop => prop.CodPecaRe5114);

        }
    }
}