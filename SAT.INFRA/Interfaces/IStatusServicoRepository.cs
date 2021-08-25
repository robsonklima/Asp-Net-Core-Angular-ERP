using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IStatusServicoRepository
    {
        void Criar(StatusServico statusServico);
        PagedList<StatusServico> ObterPorParametros(StatusServicoParameters parameters);
        void Deletar(int codigo);
        void Atualizar(StatusServico statusServico);
        StatusServico ObterPorCodigo(int codigo);
    }
}
