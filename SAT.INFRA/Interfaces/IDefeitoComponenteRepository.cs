using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDefeitoComponenteRepository
    {
        PagedList<DefeitoComponente> ObterPorParametros(DefeitoComponenteParameters parameters);
        void Criar(DefeitoComponente defeito);
        void Deletar(int codigo);
        void Atualizar(DefeitoComponente defeito);
        DefeitoComponente ObterPorCodigo(int codigo);
        DefeitoComponente ExisteDefeitoComponente(DefeitoComponente defeito);
    }
}
