using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IOrdemServicoSTNService
    {
        ListViewModel ObterPorParametros(OrdemServicoSTNParameters parameters);
        OrdemServicoSTN Criar(OrdemServicoSTN ordem);
        void Deletar(int codigo);
        OrdemServicoSTN Atualizar(OrdemServicoSTN ordem);
        OrdemServicoSTN ObterPorCodigo(int codigo);
    }
}
