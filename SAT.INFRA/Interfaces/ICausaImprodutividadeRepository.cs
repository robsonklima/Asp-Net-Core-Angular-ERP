using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ICausaImprodutividadeRepository
    {
        PagedList<CausaImprodutividade> ObterPorParametros(CausaImprodutividadeParameters parameters);
        CausaImprodutividade ObterPorCodigo(int CodCausaImprodutividade);
        void Criar(CausaImprodutividade causaImprodutividade);
        void Deletar(int CodCausaImprodutividade);
        void Atualizar(CausaImprodutividade causaImprodutividade);
    }
}
