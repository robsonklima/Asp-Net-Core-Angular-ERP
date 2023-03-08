using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OsBancadaPecasOrcamentoMap : IEntityTypeConfiguration<OsBancadaPecasOrcamento>
    {
        public void Configure(EntityTypeBuilder<OsBancadaPecasOrcamento> builder)
        {
            builder
                .ToTable("OsBancadaPecasOrcamento");

            builder
                .HasKey(i => i.CodOrcamento);

            builder
                .HasOne(prop => prop.Usuario)
                .WithMany()
                .HasForeignKey(prop => new { prop.CodUsuarioManut })
                .HasPrincipalKey(prop => new { prop.CodUsuario });                

            builder
                .HasOne(prop => prop.OSBancadaPecas)
                .WithMany()
                .HasForeignKey(prop => new { prop.CodOsbancada, prop.CodPecaRe5114 })
                .HasPrincipalKey(prop => new { prop.CodOsbancada, prop.CodPecaRe5114 });
        }
    }
}