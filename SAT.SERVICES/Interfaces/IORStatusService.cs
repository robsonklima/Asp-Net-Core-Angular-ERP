using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IORStatusService
    {
        ListViewModel ObterPorParametros(ORStatusParameters parameters);
        ORStatus Criar(ORStatus status);
        void Deletar(int codigo);
        void Atualizar(ORStatus status);
        ORStatus ObterPorCodigo(int codigo);
    }
}
