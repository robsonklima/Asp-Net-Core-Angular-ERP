using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder
                .ToTable("Usuario");

            builder
                .HasKey(prop => prop.CodUsuario);

            builder
                .HasOne(p => p.Filial)
                .WithMany()
                .HasForeignKey("CodFilial")
                .HasPrincipalKey("CodFilial");

            builder
               .HasOne(p => p.Tecnico)
               .WithOne(p => p.Usuario)
               .HasForeignKey<Usuario>("CodTecnico")
               .HasPrincipalKey<Tecnico>("CodTecnico");

            builder
                .HasOne(p => p.Cliente)
                .WithOne()
                .HasForeignKey<Usuario>("CodCliente")
                .HasPrincipalKey<Cliente>("CodCliente");

            builder
                .HasOne(p => p.Cidade)
                .WithMany()
                .HasForeignKey("CodCidade")
                .HasPrincipalKey("CodCidade");

            builder
                .HasOne(p => p.Perfil)
                .WithMany()
                .HasForeignKey("CodPerfil")
                .HasPrincipalKey("CodPerfil");

            builder
                .HasOne(p => p.Cargo)
                .WithMany()
                .HasForeignKey("CodCargo")
                .HasPrincipalKey("CodCargo");

            builder
                .HasOne(p => p.Turno)
                .WithMany()
                .HasForeignKey("CodTurno")
                .HasPrincipalKey("CodTurno");

            builder
                .HasMany(p => p.Localizacoes)
                .WithOne()
                .HasForeignKey(i => i.CodUsuario)
                .HasPrincipalKey(i => i.CodUsuario);

            builder
                .HasMany(p => p.PontosPeriodoUsuario)
                .WithOne()
                .HasForeignKey(i => i.CodUsuario)
                .HasPrincipalKey(i => i.CodUsuario);
            builder
                .HasMany(p => p.PontosUsuario)
                .WithOne()
                .HasForeignKey(i => i.CodUsuario)
                .HasPrincipalKey(i => i.CodUsuario);

            builder
                .HasOne(p => p.FilialPonto)
                .WithMany()
                .HasForeignKey("CodFilialPonto")
                .HasPrincipalKey("CodFilial");

            builder
                .Property(p => p.FoneParticular)
                .HasColumnName("Fax");
        }
    }
}