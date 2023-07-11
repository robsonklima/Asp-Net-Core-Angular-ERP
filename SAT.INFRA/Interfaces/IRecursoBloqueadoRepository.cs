using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IRecursoBloqueadoRepository
    {
        PagedList<RecursoBloqueado> ObterPorParametros(RecursoBloqueadoParameters data);
        RecursoBloqueado Criar(RecursoBloqueado data);
        RecursoBloqueado Atualizar(RecursoBloqueado data);
        RecursoBloqueado Deletar(int cod);
        RecursoBloqueado ObterPorCodigo(int cod);
    }
}
