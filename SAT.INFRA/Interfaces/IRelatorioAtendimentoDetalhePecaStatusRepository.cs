using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IRelatorioAtendimentoDetalhePecaStatusRepository {
        PagedList<RelatorioAtendimentoDetalhePecaStatus> ObterPorParametros(RelatorioAtendimentoDetalhePecaStatusParameters parameters);
        RelatorioAtendimentoDetalhePecaStatus ObterPorCodigo(int CodRATDetalhesPecasStatus);
        void Criar(RelatorioAtendimentoDetalhePecaStatus relatorioAtendimentoDetalhePecaStatus);
        void Deletar(int CodRATDetalhesPecasStatus);
        void Atualizar(RelatorioAtendimentoDetalhePecaStatus relatorioAtendimentoDetalhePecaStatus);
    }
}