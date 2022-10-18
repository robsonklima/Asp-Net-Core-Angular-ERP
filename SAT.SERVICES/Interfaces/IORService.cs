using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IORService
    {
        ListViewModel ObterPorParametros(ORParameters parameters);
        OR Criar(OR or);
        void Deletar(int codigo);
        void Atualizar(OR or);
        OR ObterPorCodigo(int codigo);
    }
}
