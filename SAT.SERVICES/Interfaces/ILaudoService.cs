using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ILaudoService
    {
        ListViewModel ObterPorParametros(LaudoParameters parameters);
        Laudo ObterPorCodigo(int codigo);
        void Atualizar(Laudo laudo);
    }
}