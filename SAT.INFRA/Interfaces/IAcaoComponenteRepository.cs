using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IAcaoComponenteRepository
    {
        PagedList<AcaoComponente> ObterPorParametros(AcaoComponenteParameters parameters);
        void Criar(AcaoComponente acao);
        void Deletar(int codigo);
        void Atualizar(AcaoComponente acao);
        AcaoComponente ObterPorCodigo(int codigo);
        AcaoComponente ExisteAcaoComponente(AcaoComponente acao);
    }
}
