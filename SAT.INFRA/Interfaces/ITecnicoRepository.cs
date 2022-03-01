using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.Entities.Params;
using System.Linq;

namespace SAT.INFRA.Interfaces
{
    public interface ITecnicoRepository
    {
        PagedList<Tecnico> ObterPorParametros(TecnicoParameters parameters);
        IQueryable<Tecnico> ObterQuery(TecnicoParameters parameters);
        void Criar(Tecnico tecnico);
        void Atualizar(Tecnico tecnico);
        void Deletar(int codTecnico);
        Tecnico ObterPorCodigo(int codigo);
    }
}
