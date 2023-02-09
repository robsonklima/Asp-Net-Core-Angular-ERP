using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IOrcamentoPecasEspecService
    {
        ListViewModel ObterPorParametros(OrcamentoPecasEspecParameters parameters);
        OrcamentoPecasEspec Criar(OrcamentoPecasEspec orcamentoPecasEspec);
        void Deletar(int codigo);
        void Atualizar(OrcamentoPecasEspec orcamentoPecasEspec);
        OrcamentoPecasEspec ObterPorCodigo(int codigo);
    }
}
