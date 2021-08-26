using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IAgendamentoService
    {
        ListViewModel ObterPorParametros(AgendamentoParameters parameters);
        Agendamento Criar(Agendamento agendamento);
        void Deletar(int codigo);
        void Atualizar(Agendamento agendamento);
        Agendamento ObterPorCodigo(int codigo);
    }
}
