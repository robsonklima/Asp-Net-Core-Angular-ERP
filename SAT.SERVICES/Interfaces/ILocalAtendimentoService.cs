using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ILocalAtendimentoService
    {
        ListViewModel ObterPorParametros(LocalAtendimentoParameters parameters);
        LocalAtendimento Criar(LocalAtendimento localAtendimento);
        void Deletar(int codigo);
        void Atualizar(LocalAtendimento localAtendimento);
        LocalAtendimento ObterPorCodigo(int codigo);
    }
}
