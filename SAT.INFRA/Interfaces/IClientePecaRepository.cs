using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Interfaces
{
    public interface IClientePecaRepository
    {
        PagedList<ClientePeca> ObterPorParametros(ClientePecaParameters parameters);
        IQueryable<ClientePeca> ObterQuery(ClientePecaParameters parameters);
        void Criar(ClientePeca peca);
        void Atualizar(ClientePeca peca);
        void Deletar(int codPeca);
        ClientePeca ObterPorCodigo(int codigo);
    }
}
