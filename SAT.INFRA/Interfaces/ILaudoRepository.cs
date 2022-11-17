using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ILaudoRepository
    {
        Laudo ObterPorCodigo(int codigo);
        PagedList<Laudo> ObterPorParametros(LaudoParameters parameters);
        void Atualizar(Laudo laudo);
    }
}
