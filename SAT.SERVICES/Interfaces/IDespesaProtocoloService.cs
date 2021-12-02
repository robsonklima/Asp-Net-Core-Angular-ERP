using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDespesaProtocoloService
    {
        ListViewModel ObterPorParametros(DespesaProtocoloParameters parameters);
        DespesaProtocolo Criar(DespesaProtocolo protocolo);
        void Deletar(int codigo);
        void Atualizar(DespesaProtocolo protocolo);
        DespesaProtocolo ObterPorCodigo(int codigo);
    }
}