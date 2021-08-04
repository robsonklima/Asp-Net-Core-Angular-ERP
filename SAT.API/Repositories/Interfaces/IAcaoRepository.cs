using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
{
    public interface IAcaoRepository
    {
        PagedList<Acao> ObterPorParametros(AcaoParameters parameters);
        void Criar(Acao acao);
        void Deletar(int codigo);
        void Atualizar(Acao acao);
        Acao ObterPorCodigo(int codigo);
    }
}
