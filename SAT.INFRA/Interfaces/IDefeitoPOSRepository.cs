using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDefeitoPOSRepository
    {
        PagedList<DefeitoPOS> ObterPorParametros(DefeitoPOSParameters parameters);
        DefeitoPOS Criar(DefeitoPOS d);
        DefeitoPOS Deletar(int codigo);
        DefeitoPOS Atualizar(DefeitoPOS d);
        DefeitoPOS ObterPorCodigo(int codigo);
    }
}
