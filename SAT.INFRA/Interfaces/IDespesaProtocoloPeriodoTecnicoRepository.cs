using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDespesaProtocoloPeriodoTecnicoRepository
    {
        PagedList<DespesaProtocoloPeriodoTecnico> ObterPorParametros(DespesaProtocoloPeriodoTecnicoParameters parameters);
        void Criar(DespesaProtocoloPeriodoTecnico despesa);
        void Deletar(int codigo);
        void Atualizar(DespesaProtocoloPeriodoTecnico despesa);
        DespesaProtocoloPeriodoTecnico ObterPorCodigo(int codigo);
    }
}