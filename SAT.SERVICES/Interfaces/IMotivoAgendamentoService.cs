using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IMotivoAgendamentoService
    {
        ListViewModel ObterPorParametros(MotivoAgendamentoParameters parameters);
        MotivoAgendamento Criar(MotivoAgendamento motivoAgendamento);
        void Deletar(int codigo);
        void Atualizar(MotivoAgendamento motivoAgendamento);
        MotivoAgendamento ObterPorCodigo(int codigo);
    }
}
