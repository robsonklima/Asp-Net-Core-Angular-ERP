using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDespesaProtocoloRepository
    {
        PagedList<DespesaProtocolo> ObterPorParametros(DespesaProtocoloParameters parameters);
        DespesaProtocolo Criar(DespesaProtocolo protocolo);
        void Atualizar(DespesaProtocolo protocolo);
        void Deletar(int codigo);
        DespesaProtocolo ObterPorCodigo(int codigo);
    }
}