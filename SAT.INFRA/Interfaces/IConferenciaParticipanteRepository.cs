using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IConferenciaParticipanteRepository
    {
        PagedList<ConferenciaParticipante> ObterPorParametros(ConferenciaParticipanteParameters parameters);
        ConferenciaParticipante ObterPorCodigo(int codigo);
        void Criar(ConferenciaParticipante participante);
        void Deletar(int codigo);
    }
}
