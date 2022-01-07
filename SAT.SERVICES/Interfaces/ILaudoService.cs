using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface ILaudoService
    {
        Laudo ObterPorCodigo(int codigo);
    }
}