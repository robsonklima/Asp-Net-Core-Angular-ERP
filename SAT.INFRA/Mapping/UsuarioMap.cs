using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;
using System.Linq;

namespace SAT.INFRA.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(prop => prop.CodUsuario);

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
            .HasOne(p => p.UsuarioSeguranca)
            .WithMany()
            .HasForeignKey("CodUsuario")
            .HasPrincipalKey("CodUsuario");

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
                .HasOne(p => p.Setor)
                .WithMany()
                .HasForeignKey("CodSetor")
                .HasPrincipalKey("CodSetor");

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
                .HasMany(p => p.RecursosBloqueados)
                .WithOne()
                .HasForeignKey(prop => new { prop.CodSetor, prop.CodPerfil })
                .HasPrincipalKey(prop => new { prop.CodSetor, prop.CodPerfil });

            builder
                 .Property(p => p.CodUsuario)
                 .HasConversion(v => v.ToString(),
                                v => v.ToLower());

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
               .HasMany(p => p.FiltroUsuario)
               .WithOne()
               .HasForeignKey(i => i.CodUsuario)
               .HasPrincipalKey(i => i.CodUsuario);

            builder
                .HasOne(p => p.FilialPonto)
                .WithMany()
                .HasForeignKey("CodFilialPonto")
                .HasPrincipalKey("CodFilial");

            builder
                .HasMany(i => i.UsuarioDispositivos)
                .WithOne()
                .HasForeignKey(i => new { i.CodUsuario });

            builder
                .Property(p => p.FoneParticular)
                .HasColumnName("Fax");

            builder
                .HasMany(p => p.Acessos)
                .WithOne()
                .HasForeignKey(i => i.CodUsuario)
                .HasPrincipalKey(i => i.CodUsuario);

            builder
                .HasOne(p => p.Transportadora)
                .WithMany()
                .HasForeignKey(prop => prop.CodTransportadora)
                .HasPrincipalKey(prop => prop.CodTransportadora);

            builder
                .HasMany(i => i.NavegacoesConfiguracao)
                .WithOne()
                .HasForeignKey(prop => new { prop.CodPerfil, prop.CodSetor })
                .HasPrincipalKey((prop => new { prop.CodPerfil, prop.CodSetor }));

            builder.Ignore(p => p.Foto);
        }
    }
}