using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace SAT.INFRA.Interfaces
{
    public interface IClienteRepository
    {
        PagedList<Cliente> ObterPorParametros(ClienteParameters parameters);
        IQueryable<Cliente> ObterPorQuery(ClienteParameters parameters);
        IEnumerable<Cliente> ObterTodos();
        Cliente ObterPorCodigo(int codigo);
    }
}
