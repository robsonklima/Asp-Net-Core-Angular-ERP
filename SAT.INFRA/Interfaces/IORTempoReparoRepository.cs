using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IORTempoReparoRepository
    {
        PagedList<ORTempoReparo> ObterPorParametros(ORTempoReparoParameters parameters);
        void Criar(ORTempoReparo tr);
        void Atualizar(ORTempoReparo tr);
        void Deletar(int codigo);
        ORTempoReparo ObterPorCodigo(int codigo);
    }
}
