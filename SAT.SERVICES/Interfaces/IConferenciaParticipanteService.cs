using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IConferenciaParticipanteService
    {
        ListViewModel ObterPorParametros(ConferenciaParticipanteParameters parameters);
        ConferenciaParticipante Criar(ConferenciaParticipante participante);
        void Deletar(int codigo);
        ConferenciaParticipante ObterPorCodigo(int codigo);
    }
}