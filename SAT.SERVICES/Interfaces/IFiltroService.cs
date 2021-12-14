using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IFiltroService
    {
        ListViewModel ObterPorParametros(AcaoParameters parameters);
        FiltroUsuario Criar(FiltroUsuario filtroUsuario);
        void Deletar(int codigo);
        void Atualizar(FiltroUsuario filtroUsuario);
        FiltroUsuario ObterPorCodigo(int codigo);
    }
}
