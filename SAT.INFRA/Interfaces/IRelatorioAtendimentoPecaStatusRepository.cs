using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces {
    public interface IRelatorioAtendimentoPecaStatusRepository {
        PagedList<RelatorioAtendimentoPecaStatus> ObterPorParametros(RelatorioAtendimentoPecaStatusParameters parameters);
        RelatorioAtendimentoPecaStatus ObterPorCodigo(int CodRatpecasStatus);
        void Criar(RelatorioAtendimentoPecaStatus relatorioAtendimentoPecaStatus);
        void Deletar(int codigo);
        void Atualizar(RelatorioAtendimentoPecaStatus relatorioAtendimentoPecaStatus);
    }
}