using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ILaudoSituacaoService
    {
        ListViewModel ObterPorParametros(LaudoSituacaoParameters parameters);
        LaudoSituacao ObterPorCodigo(int codigo);
        void Atualizar(LaudoSituacao laudoSituacao);
        LaudoSituacao Criar(LaudoSituacao laudoSituacao);
    }
}