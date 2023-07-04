using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class CidadeMap : IEntityTypeConfiguration<Cidade>
    {
        public void Configure(EntityTypeBuilder<Cidade> builder)
        {
            builder.ToTable("Cidade");
            builder.HasKey(prop => prop.CodCidade);
           
            builder
                .HasOne(prop => prop.UnidadeFederativa)
                .WithMany()
                .HasForeignKey(prop => prop.CodUF)
                .HasPrincipalKey(prop => prop.CodUF);

            builder.Ignore(p => p.LatitudeMetros);
            builder.Ignore(p => p.LongitudeMetros);
            builder.Ignore(p => p.Horas_RAcesso);
        }
    }
}