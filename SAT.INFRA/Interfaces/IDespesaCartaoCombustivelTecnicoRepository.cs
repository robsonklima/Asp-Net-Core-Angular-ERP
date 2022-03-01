using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDespesaCartaoCombustivelTecnicoRepository
    {
        PagedList<DespesaCartaoCombustivelTecnico> ObterPorParametros(DespesaCartaoCombustivelTecnicoParameters parameters);
        void Criar(DespesaCartaoCombustivelTecnico despesa);
        void Atualizar(DespesaCartaoCombustivelTecnico acao);
    }
}