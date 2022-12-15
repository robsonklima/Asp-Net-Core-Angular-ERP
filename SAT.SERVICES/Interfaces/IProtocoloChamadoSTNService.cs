using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface IProtocoloChamadoSTNService
    {
        ListViewModel ObterPorParametros(ProtocoloChamadoSTNParameters parameters);
        ProtocoloChamadoSTN ObterPorCodigo(int codigo);
        void Criar(ProtocoloChamadoSTN protocoloChamadoSTN);
        void Deletar(int codProtocoloChamadoSTN);
        void Atualizar(ProtocoloChamadoSTN protocoloChamadoSTN);
    }
}
