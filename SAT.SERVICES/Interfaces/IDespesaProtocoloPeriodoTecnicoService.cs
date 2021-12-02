using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDespesaProtocoloPeriodoTecnicoService
    {
        ListViewModel ObterPorParametros(DespesaProtocoloPeriodoTecnicoParameters parameters);
        DespesaProtocoloPeriodoTecnico Criar(DespesaProtocoloPeriodoTecnico despesa);
        void Deletar(int codigo);
        void Atualizar(DespesaProtocoloPeriodoTecnico despesa);
        DespesaProtocoloPeriodoTecnico ObterPorCodigo(int codigo);
        DespesaProtocoloPeriodoTecnico ObterPorCodigoPeriodoTecnico(int codigo);
    }
}