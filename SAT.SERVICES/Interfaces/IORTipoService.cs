using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IORTipoService
    {
        ListViewModel ObterPorParametros(ORTipoParameters parameters);
        ORTipo Criar(ORTipo tipo);
        void Deletar(int codigo);
        void Atualizar(ORTipo tipo);
        ORTipo ObterPorCodigo(int codigo);
    }
}
