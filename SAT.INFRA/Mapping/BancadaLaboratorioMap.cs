using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class BancadaLaboratorioMap : IEntityTypeConfiguration<BancadaLaboratorio>
    {
        public void Configure(EntityTypeBuilder<BancadaLaboratorio> builder)
        {
            builder.ToTable("BancadaLaboratorio");
            
            builder.
                HasKey(i => new { i.CodBancadaLaboratorio });

            builder
                .HasOne(prop => prop.Usuario)
                .WithMany()
                .HasForeignKey(prop => prop.CodUsuario)
                .HasPrincipalKey(prop => prop.CodUsuario);

            builder
                .HasOne(prop => prop.UsuarioCadastro)
                .WithMany()
                .HasForeignKey(prop => prop.CodUsuarioCad)
                .HasPrincipalKey(prop => prop.CodUsuario);
        }
    }
}