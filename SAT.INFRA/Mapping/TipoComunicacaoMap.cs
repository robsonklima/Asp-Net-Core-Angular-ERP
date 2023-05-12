using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class TipoComunicacaoMap : IEntityTypeConfiguration<TipoComunicacao>
    {
        public void Configure(EntityTypeBuilder<TipoComunicacao> builder)
        {
            builder.ToTable("TipoComunicacao");
            builder.HasKey(prop => prop.CodTipoComunicacao);
            builder.Property(prop => prop.Tipo).HasColumnName("TipoComunicacao");
        }
    }
}