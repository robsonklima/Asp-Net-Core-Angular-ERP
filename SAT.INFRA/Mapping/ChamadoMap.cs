using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ChamadoMap : IEntityTypeConfiguration<Chamado>
    {
        public void Configure(EntityTypeBuilder<Chamado> builder)
        {
            builder.ToTable("Chamado");
            builder.HasKey(prop => prop.CodOS);
           
            builder
                .HasOne(prop => prop.ChamadoDadosAdicionais)
                .WithMany()
                .HasForeignKey(prop => prop.CodChamado)
                .HasPrincipalKey(prop => prop.CodChamado);
           
            builder
                .HasOne(prop => prop.OperadoraTelefonia)
                .WithMany()
                .HasForeignKey(prop => prop.CodOperadoraTelefonia)
                .HasPrincipalKey(prop => prop.CodOperadoraTelefonia);

           builder
                .HasOne(e => e.DefeitoPOS)
                .WithMany()
                .HasForeignKey(prop => prop.CodDefeitoPOS)
                .HasPrincipalKey(prop => prop.CodDefeitoPOS)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}