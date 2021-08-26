using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ITipoIntervencaoService
    {
        ListViewModel ObterPorParametros(TipoIntervencaoParameters parameters);
        TipoIntervencao Criar(TipoIntervencao tipoIntervencao);
        void Deletar(int codigo);
        void Atualizar(TipoIntervencao tipoIntervencao);
        TipoIntervencao ObterPorCodigo(int codigo);
    }
}
