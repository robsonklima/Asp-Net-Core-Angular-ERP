using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ILaudoSituacaoRepository
    {
        LaudoSituacao ObterPorCodigo(int codigo);
        PagedList<LaudoSituacao> ObterPorParametros(LaudoSituacaoParameters parameters);
        void Atualizar(LaudoSituacao laudoSituacao);
        LaudoSituacao Criar(LaudoSituacao laudoSituacao);
    }
}
