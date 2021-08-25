using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.INFRA.Interfaces
{
    public interface IClienteRepository
    {
        PagedList<Cliente> ObterPorParametros(ClienteParameters parameters);
        IEnumerable<Cliente> ObterTodos();
        Cliente ObterPorCodigo(int codigo);
    }
}
