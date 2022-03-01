using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IRegiaoService
    {
        ListViewModel ObterPorParametros(RegiaoParameters parameters);
        Regiao Criar(Regiao regiao);
        void Deletar(int codigo);
        void Atualizar(Regiao regiao);
        Regiao ObterPorCodigo(int codigo);
    }
}
