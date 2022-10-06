using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IProtocoloSTNRepository
    {
        PagedList<ProtocoloSTN> ObterPorParametros(ProtocoloSTNParameters parameters);
        ProtocoloSTN Criar(ProtocoloSTN ProtocoloSTN);
        void Atualizar(ProtocoloSTN ProtocoloSTN);
        void Deletar(int codProtocoloSTN);
        ProtocoloSTN ObterPorCodigo(int codigo);
    }
}
