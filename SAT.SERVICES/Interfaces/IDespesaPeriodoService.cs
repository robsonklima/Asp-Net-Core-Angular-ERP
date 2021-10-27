using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDespesaPeriodoService
    {
        ListViewModel ObterPorParametros(DespesaPeriodoParameters parameters);
        DespesaPeriodo Criar(DespesaPeriodo despesa);
        void Deletar(int codigo);
        void Atualizar(DespesaPeriodo despesa);
        DespesaPeriodo ObterPorCodigo(int codigo);
    }
}