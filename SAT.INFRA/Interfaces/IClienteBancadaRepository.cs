using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IClienteBancadaRepository
    {
        PagedList<ClienteBancada> ObterPorParametros(ClienteBancadaParameters parameters);
        void Criar(ClienteBancada clienteBancada);
        void Atualizar(ClienteBancada clienteBancada);
        void Deletar(int codClienteBancada);
        ClienteBancada ObterPorCodigo(int codigo);
    }
}
