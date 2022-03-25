using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IAcaoRepository
    {
        PagedList<Acao> ObterPorParametros(AcaoParameters parameters);
        void Criar(Acao acao);
        void Deletar(int codigo);
        void Atualizar(Acao acao);
        Acao ObterPorCodigo(int codigo);
        PagedList<AcaoComponente> ObterListaAcaoComponente(AcaoParameters parameters);
        AcaoComponente ObterAcaoComponentePorCodigo(int codigo);
    }
}
