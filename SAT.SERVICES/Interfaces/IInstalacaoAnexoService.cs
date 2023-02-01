using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IInstalacaoAnexoService
    {
        InstalacaoAnexo Criar(InstalacaoAnexo instalacaoAnexo);
        void Deletar(int codigo);
        InstalacaoAnexo ObterPorCodigo(int codigo);
        ListViewModel ObterPorParametros(InstalacaoAnexoParameters parameters);
        void AlterarInstalacaoAnexoPerfil(ImagemPerfilModel model);
        ImagemPerfilModel BuscarInstalacaoAnexoUsuario(string codUsuario);
    }
}
