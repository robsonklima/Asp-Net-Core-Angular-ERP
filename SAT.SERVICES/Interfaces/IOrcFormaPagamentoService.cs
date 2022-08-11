using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces {
    public interface IOrcFormaPagamentoService {
        ListViewModel ObterPorParametros(OrcFormaPagamentoParameters parameters);
    }
}