using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDispBBBloqueioOSService
    {
        ListViewModel ObterPorParametros(DispBBBloqueioOSParameters parameters);
        DispBBBloqueioOS Criar(DispBBBloqueioOS dispBBBloqueioOS);
        void Deletar(int codigo);
        void Atualizar(DispBBBloqueioOS dispBBBloqueioOS);
        DispBBBloqueioOS ObterPorCodigo(int codigo);
    }
}