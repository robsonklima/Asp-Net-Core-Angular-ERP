using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IAnexoService
    {
        Foto Criar(Foto foto);
        void Deletar(int codigo);
        Foto ObterPorCodigo(int codigo);
        ListViewModel ObterPorParametros(FotoParameters parameters);
        void AlterarFotoPerfil(ImagemPerfilModel model);
        ImagemPerfilModel BuscarFotoUsuario(string codUsuario);
    }
}
