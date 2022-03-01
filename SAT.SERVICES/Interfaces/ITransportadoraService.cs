using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ITransportadoraService
    {
        ListViewModel ObterPorParametros(TransportadoraParameters parameters);
        Transportadora Criar(Transportadora transportadora);
        void Deletar(int codigo);
        void Atualizar(Transportadora transportadora);
        Transportadora ObterPorCodigo(int codigo);
    }
}
