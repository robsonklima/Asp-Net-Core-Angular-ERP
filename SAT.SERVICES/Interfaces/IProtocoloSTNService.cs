using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IProtocoloSTNService
    {
        ListViewModel ObterPorParametros(ProtocoloSTNParameters parameters);
        ProtocoloSTN Criar(ProtocoloSTN protocoloSTN);
        void Deletar(int codigo);
        void Atualizar(ProtocoloSTN protocoloSTN);
        ProtocoloSTN ObterPorCodigo(int codigo);
    }
}
