using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class DespesaConfiguracaoCombustivelMap : IEntityTypeConfiguration<DespesaConfiguracaoCombustivel>
    {
        public void Configure(EntityTypeBuilder<DespesaConfiguracaoCombustivel> builder)
        {
            builder
                .ToTable("DespesaConfiguracaoCombustivel");

            builder
                .HasKey(i => i.CodDespesaConfiguracaoCombustivel);

             builder
                .HasOne(prop => prop.UsuarioCadastro)
                .WithMany()
                .HasForeignKey(prop => prop.CodUsuarioCad)
                .HasPrincipalKey(prop => prop.CodUsuario);

             builder
                .HasOne(prop => prop.UsuarioManutencao)
                .WithMany()
                .HasForeignKey(prop => prop.CodUsuarioManut)
                .HasPrincipalKey(prop => prop.CodUsuario);
             
             builder
                .HasOne(prop => prop.Filial)
                .WithMany()
                .HasForeignKey(prop => prop.CodFilial)
                .HasPrincipalKey(prop => prop.CodFilial);

             builder
                .HasOne(prop => prop.UnidadeFederativa)
                .WithMany()
                .HasForeignKey(prop => prop.CodUf)
                .HasPrincipalKey(prop => prop.CodUF);
        }
    }
}