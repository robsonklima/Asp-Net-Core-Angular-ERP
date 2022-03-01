using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IInstalacaoRessalvaService
    {
        ListViewModel ObterPorParametros(InstalacaoRessalvaParameters parameters);
        InstalacaoRessalva Criar(InstalacaoRessalva instalLote);
        void Deletar(int codigo);
        void Atualizar(InstalacaoRessalva instalLote);
        InstalacaoRessalva ObterPorCodigo(int codigo);
    }
}
