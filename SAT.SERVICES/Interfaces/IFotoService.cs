using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IFotoService
    {
        Foto Criar(Foto foto);
        void Deletar(int codigo);
        Foto ObterPorCodigo(int codigo);
        ListViewModel ObterPorParametros(FotoParameters parameters);
    }
}
