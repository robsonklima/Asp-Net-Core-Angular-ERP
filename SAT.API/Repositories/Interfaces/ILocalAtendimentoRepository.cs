using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
{
    public interface ILocalAtendimentoRepository
    {
        void Criar(LocalAtendimento localAtendimento);
        PagedList<LocalAtendimento> ObterPorParametros(LocalAtendimentoParameters parameters);
        void Deletar(int codigo);
        void Atualizar(LocalAtendimento localAtendimento);
        LocalAtendimento ObterPorCodigo(int codigo);
    }
}
