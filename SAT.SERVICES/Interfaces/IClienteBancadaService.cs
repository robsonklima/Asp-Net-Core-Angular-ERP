using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IClienteBancadaService
    {
        ListViewModel ObterPorParametros(ClienteBancadaParameters parameters);
        ClienteBancada Criar(ClienteBancada clienteBancada);
        void Deletar(int codigo);
        void Atualizar(ClienteBancada clienteBancada);
        ClienteBancada ObterPorCodigo(int codigo);
    }
}
