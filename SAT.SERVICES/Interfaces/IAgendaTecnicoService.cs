using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IAgendaTecnicoService
    {
        AgendaTecnico[] ObterAgendaTecnico(AgendaTecnicoParameters parameters);
        AgendaTecnico CriarAgendaTecnico(int codOS);
        AgendaTecnico ObterPorCodigo(int codigo);
        AgendaTecnico Atualizar(AgendaTecnico agenda);
        void Deletar(int codigo);
        void Criar(AgendaTecnico agenda);
    }
}