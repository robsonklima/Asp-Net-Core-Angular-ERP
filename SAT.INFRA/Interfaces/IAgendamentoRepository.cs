using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IAgendamentoRepository
    {
        PagedList<Agendamento> ObterPorParametros(AgendamentoParameters parameters);
        void Criar(Agendamento agendamento);
        void Deletar(int codigo);
        void Atualizar(Agendamento agendamento);
        Agendamento ObterPorCodigo(int codigo);
    }
}
