using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.SERVICES.Interfaces
{
    public interface IAcaoService
    {
        PagedList<Acao> ObterPorParametros(AcaoParameters parameters);
        void Criar(Acao acao);
        void Deletar(int codigo);
        void Atualizar(Acao acao);
        Acao ObterPorCodigo(int codigo);
    }
}
