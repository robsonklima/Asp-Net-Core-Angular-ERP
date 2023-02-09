using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IOsBancadaPecasOrcamentoService
    {
        ListViewModel ObterPorParametros(OsBancadaPecasOrcamentoParameters parameters);
        OsBancadaPecasOrcamento Criar(OsBancadaPecasOrcamento osBancadaPecasOrcamento);
        void Deletar(int codigo);
        void Atualizar(OsBancadaPecasOrcamento osBancadaPecasOrcamento);
        OsBancadaPecasOrcamento ObterPorCodigo(int codigo);
    }
}
