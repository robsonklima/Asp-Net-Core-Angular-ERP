using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IVersaoService
    {
        ListViewModel ObterPorParametros(VersaoParameters parameters);
        Versao Criar(Versao Versao);
        void Deletar(int codigo);
        void Atualizar(Versao Versao);
        Versao ObterPorCodigo(int codigo);
    }
}
