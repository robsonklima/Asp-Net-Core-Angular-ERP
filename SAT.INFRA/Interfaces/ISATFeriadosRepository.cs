using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ISATFeriadosRepository
    {
        PagedList<SATFeriados> ObterPorParametros(SATFeriadosParameters parameters);
        void Criar(SATFeriados satFeriado);
        void Atualizar(SATFeriados satFeriado);
        void Deletar(int codSATFeriados);
        SATFeriados ObterPorCodigo(int codigo);
    }
}
