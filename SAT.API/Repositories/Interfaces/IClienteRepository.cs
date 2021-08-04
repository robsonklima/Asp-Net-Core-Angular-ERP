using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        PagedList<Cliente> ObterPorParametros(ClienteParameters parameters);
        Cliente ObterPorCodigo(int codigo);
    }
}
