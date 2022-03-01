using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IStatusServicoService
    {
        ListViewModel ObterPorParametros(StatusServicoParameters parameters);
        StatusServico Criar(StatusServico statusServico);
        void Deletar(int codigo);
        void Atualizar(StatusServico statusServico);
        StatusServico ObterPorCodigo(int codigo);
    }
}
