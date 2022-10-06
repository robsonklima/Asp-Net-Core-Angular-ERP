using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IStatusServicoSTNService
    {
        ListViewModel ObterPorParametros(StatusServicoSTNParameters parameters);
        StatusServicoSTN Criar(StatusServicoSTN statusServicoSTN);
        void Deletar(int codigo);
        void Atualizar(StatusServicoSTN statusServicoSTN);
        StatusServicoSTN ObterPorCodigo(int codigo);
    }
}
