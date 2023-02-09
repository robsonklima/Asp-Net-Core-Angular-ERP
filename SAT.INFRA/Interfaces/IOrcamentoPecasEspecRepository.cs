using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IOrcamentoPecasEspecRepository
    {
        PagedList<OrcamentoPecasEspec> ObterPorParametros(OrcamentoPecasEspecParameters parameters);
        void Criar(OrcamentoPecasEspec orcamentoPecasEspec);
        void Atualizar(OrcamentoPecasEspec orcamentoPecasEspec);
        void Deletar(int codOrcamentoPecasEspec);
        OrcamentoPecasEspec ObterPorCodigo(int codigo);
    }
}
