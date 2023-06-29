using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ISATFeriadoRepository
    {
        PagedList<SATFeriado> ObterPorParametros(SATFeriadoParameters parameters);
        SATFeriado Criar(SATFeriado feriado);
        SATFeriado Atualizar(SATFeriado feriado);
        SATFeriado Deletar(int cod);
        SATFeriado ObterPorCodigo(int cod);
    }
}
