using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IInstalacaoService
    {
        ListViewModel ObterPorParametros(InstalacaoParameters parameters);
        Instalacao Criar(Instalacao instalacao);
        void Deletar(int codigo);
        void Atualizar(Instalacao instalacao);
        Instalacao ObterPorCodigo(int codigo);
    }
}
