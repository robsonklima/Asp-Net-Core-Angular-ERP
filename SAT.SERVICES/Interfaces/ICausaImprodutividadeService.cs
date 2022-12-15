using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface ICausaImprodutividadeService
    {
        ListViewModel ObterPorParametros(CausaImprodutividadeParameters parameters);
        CausaImprodutividade ObterPorCodigo(int codigo);
        void Criar(CausaImprodutividade causaImprodutividade);
        void Deletar(int codCausaImprodutividade);
        void Atualizar(CausaImprodutividade causaImprodutividade);
    }
}
