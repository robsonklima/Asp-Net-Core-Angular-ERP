using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDespesaAdiantamentoPeriodoService
    {
        ListViewModel ObterPorParametros(DespesaAdiantamentoPeriodoParameters parameters);
        ListViewModel ObterConsultaTecnicos(DespesaAdiantamentoPeriodoParameters parameters);
        DespesaAdiantamentoPeriodo Criar(DespesaAdiantamentoPeriodo despesa);
        void Deletar(int codigo);
        void Atualizar(DespesaAdiantamentoPeriodo despesa);
        DespesaAdiantamentoPeriodo ObterPorCodigo(int codigo);
        List<DespesaAdiantamentoPeriodo> ObterDespesasPeriodoAdiantamentos(DespesaAdiantamentoPeriodoParameters parameters);
    }
}