using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IORRepository
    {
        PagedList<OR> ObterPorParametros(ORParameters parameters);
        void Criar(OR or);
        void Atualizar(OR or);
        void Deletar(int codigo);
        OR ObterPorCodigo(int codigo);
    }
}
