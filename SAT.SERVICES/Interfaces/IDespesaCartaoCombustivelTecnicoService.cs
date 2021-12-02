using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDespesaCartaoCombustivelTecnicoService
    {
        ListViewModel ObterPorParametros(DespesaCartaoCombustivelTecnicoParameters parameters);
        DespesaCartaoCombustivelTecnico Criar(DespesaCartaoCombustivelTecnico despesa);
        void Atualizar(DespesaCartaoCombustivelTecnico acao);
    }
}