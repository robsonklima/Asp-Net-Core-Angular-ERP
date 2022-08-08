using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ContratoPedidoConfMap : IEntityTypeConfiguration<ContratoPedidoConf>
    {
        public void Configure(EntityTypeBuilder<ContratoPedidoConf> builder)
        {
            builder.ToTable("ContratoPedidoConf");
            builder.HasKey(i => i.CodContratoPedidoConf);
        }
    }
}