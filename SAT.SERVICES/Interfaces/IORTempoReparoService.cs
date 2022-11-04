using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IORTempoReparoService
    {
        ListViewModel ObterPorParametros(ORTempoReparoParameters parameters);
        ORTempoReparo Criar(ORTempoReparo tr);
        void Deletar(int codigo);
        void Atualizar(ORTempoReparo tr);
        ORTempoReparo ObterPorCodigo(int codigo);
    }
}
