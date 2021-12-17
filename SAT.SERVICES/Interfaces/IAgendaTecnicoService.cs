using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IAgendaTecnicoService
    {
        AgendaTecnico[] ObterAgendaTecnico(AgendaTecnicoParameters parameters);
        void OrdenarAgendaTecnico(AgendaTecnicoParameters parameters);
        AgendaTecnico CriarAgendaTecnico(int codOS, int codTecnico);
        void DeletarAgendaTecnico(int codOS, int codTecnico);
        AgendaTecnico ObterPorCodigo(int codigo);
        ListViewModel ObterPorParametros(AgendaTecnicoParameters parameters);
        AgendaTecnico Atualizar(AgendaTecnico agenda);
        void Deletar(int codigo);
        void Criar(AgendaTecnico agenda);
    }
}