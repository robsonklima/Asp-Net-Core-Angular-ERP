using SAT.MODELS.Entities;

namespace SAT.INFRA.Interfaces
{
    public interface ILaudoRepository
    {
        Laudo ObterPorCodigo(int codigo);
    }
}
