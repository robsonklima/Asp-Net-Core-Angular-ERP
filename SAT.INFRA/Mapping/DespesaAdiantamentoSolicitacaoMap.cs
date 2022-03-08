using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class DespesaAdiantamentoSolicitacaoMap : IEntityTypeConfiguration<DespesaAdiantamentoSolicitacao>
    {
        public void Configure(EntityTypeBuilder<DespesaAdiantamentoSolicitacao> builder)
        {
            builder.ToTable("DespesaAdiantamentoSolicitacao");
            builder.HasKey(prop => prop.CodDespesaAdiantamentoSolicitacao);
        }
    }
}
