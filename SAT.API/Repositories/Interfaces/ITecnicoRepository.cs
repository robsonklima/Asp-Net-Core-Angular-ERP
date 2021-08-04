using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
{
    public interface ITecnicoRepository
    {
        PagedList<Tecnico> ObterPorParametros(TecnicoParameters parameters);
        void Criar(Tecnico tecnico);
        void Atualizar(Tecnico tecnico);
        void Deletar(int codTecnico);
        Tecnico ObterPorCodigo(int codigo);
    }
}
