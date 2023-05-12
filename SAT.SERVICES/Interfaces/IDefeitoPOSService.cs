using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDefeitoPOSService
    {
        ListViewModel ObterPorParametros(DefeitoPOSParameters parameters);
        DefeitoPOS Criar(DefeitoPOS d);
        DefeitoPOS Deletar(int codigo);
        DefeitoPOS Atualizar(DefeitoPOS d);
        DefeitoPOS ObterPorCodigo(int codigo);
    }
}
