using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ITransportadoraRepository
    {
        PagedList<Transportadora> ObterPorParametros(TransportadoraParameters parameters);
        void Criar(Transportadora transportadora);
        void Atualizar(Transportadora transportadora);
        void Deletar(int codTransportadora);
        Transportadora ObterPorCodigo(int codigo);
    }
}
