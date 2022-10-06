using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IStatusServicoSTNRepository
    {
        StatusServicoSTN Criar(StatusServicoSTN status);
        PagedList<StatusServicoSTN> ObterPorParametros(StatusServicoSTNParameters parameters);
        void Deletar(int codigo);
        void Atualizar(StatusServicoSTN status);
        StatusServicoSTN ObterPorCodigo(int codigo);
    }
}
