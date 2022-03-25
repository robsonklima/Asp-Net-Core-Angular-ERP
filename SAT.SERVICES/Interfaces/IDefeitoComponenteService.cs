using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDefeitoComponenteService
    {
        ListViewModel ObterPorParametros(DefeitoComponenteParameters parameters);
        DefeitoComponente Criar(DefeitoComponente defeito);
        void Deletar(int codigo);
        void Atualizar(DefeitoComponente defeito);
        DefeitoComponente ObterPorCodigo(int codigo);
    }
}
