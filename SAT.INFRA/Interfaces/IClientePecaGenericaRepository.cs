using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Interfaces
{
    public interface IClientePecaGenericaRepository
    {
        PagedList<ClientePecaGenerica> ObterPorParametros(ClientePecaGenericaParameters parameters);
        IQueryable<ClientePecaGenerica> ObterQuery(ClientePecaGenericaParameters parameters);
        void Criar(ClientePecaGenerica peca);
        void Atualizar(ClientePecaGenerica peca);
        void Deletar(int codPeca);
        ClientePecaGenerica ObterPorCodigo(int codigo);
    }
}
