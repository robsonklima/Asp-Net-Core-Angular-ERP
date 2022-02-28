using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IClienteService
    {
        ListViewModel ObterPorParametros(ClienteParameters parameters);
        Cliente Criar(Cliente cliente);
        void Deletar(int codigo);
        void Atualizar(Cliente cliente);
        Cliente ObterPorCodigo(int codigo);
    }
}
