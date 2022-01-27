using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class VersaoMap : IEntityTypeConfiguration<Versao>
    {
        public void Configure(EntityTypeBuilder<Versao> builder)
        {
            builder
                .ToTable("SatVersao");

            builder
                .HasKey(prop => prop.CodSatVersao);

            builder
                .HasMany(i => i.Alteracoes)
                .WithOne()
                .HasForeignKey(i => i.CodSatVersao)
                .HasPrincipalKey(i => i.CodSatVersao);
        }
    }
}