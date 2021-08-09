using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
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
