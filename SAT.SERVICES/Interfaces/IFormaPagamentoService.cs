using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IFormaPagamentoService
    {
        ListViewModel ObterPorParametros(FormaPagamentoParameters parameters);
    }
}
