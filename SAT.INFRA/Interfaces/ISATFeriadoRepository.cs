using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ISATFeriadoRepository
    {
        PagedList<SATFeriado> ObterPorParametros(SATFeriadoParameters parameters);
        SATFeriado Criar(SATFeriado satFeriado);
        SATFeriado Atualizar(SATFeriado satFeriado);
        SATFeriado Deletar(int codSATFeriado);
        SATFeriado ObterPorCodigo(int codigo);
    }
}
