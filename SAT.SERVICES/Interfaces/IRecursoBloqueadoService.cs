using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IRecursoBloqueadoService
    {
        ListViewModel ObterPorParametros(RecursoBloqueadoParameters data);
        RecursoBloqueado Criar(RecursoBloqueado data);
        RecursoBloqueado Deletar(int codigo);
        RecursoBloqueado Atualizar(RecursoBloqueado data);
        RecursoBloqueado ObterPorCodigo(int codigo);
    }
}
