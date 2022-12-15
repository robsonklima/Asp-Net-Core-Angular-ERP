using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IProtocoloChamadoSTNRepository
    {
        PagedList<ProtocoloChamadoSTN> ObterPorParametros(ProtocoloChamadoSTNParameters parameters);
        ProtocoloChamadoSTN ObterPorCodigo(int CodProtocoloChamadoSTN);
        void Criar(ProtocoloChamadoSTN protocoloChamadoSTN);
        void Deletar(int CodProtocoloChamadoSTN);
        void Atualizar(ProtocoloChamadoSTN protocoloChamadoSTN);
    }
}
