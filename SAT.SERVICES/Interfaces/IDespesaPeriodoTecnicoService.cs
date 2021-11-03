using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDespesaPeriodoTecnicoService
    {
        ListViewModel ObterPorParametros(DespesaPeriodoTecnicoParameters parameters);
        DespesaPeriodoTecnicoAtendimentoViewModel ObterAtendimentos(DespesaPeriodoTecnicoParameters parameters);
        DespesaPeriodoTecnico Criar(DespesaPeriodoTecnico despesa);
        void Deletar(int codigo);
        void Atualizar(DespesaPeriodoTecnico despesa);
        DespesaPeriodoTecnico ObterPorCodigo(int codigo);
    }
}