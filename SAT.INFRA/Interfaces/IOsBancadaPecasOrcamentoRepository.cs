using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IOsBancadaPecasOrcamentoRepository
    {
        PagedList<OsBancadaPecasOrcamento> ObterPorParametros(OsBancadaPecasOrcamentoParameters parameters);
        void Criar(OsBancadaPecasOrcamento osBancadaPecasOrcamento);
        void Atualizar(OsBancadaPecasOrcamento osBancadaPecasOrcamento);
        void Deletar(int codOrcamento);
        OsBancadaPecasOrcamento ObterPorCodigo(int codigo);
    }
}
