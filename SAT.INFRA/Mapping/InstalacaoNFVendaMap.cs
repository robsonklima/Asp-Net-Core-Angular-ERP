using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class InstalacaoNFVendaMap : IEntityTypeConfiguration<InstalacaoNFVenda>
    {
        public void Configure(EntityTypeBuilder<InstalacaoNFVenda> builder)
        {
            builder.ToTable("InstalNFVenda");

            builder
                .HasKey(i => new { i.CodInstalNfvenda });
            
        }
    }
}
