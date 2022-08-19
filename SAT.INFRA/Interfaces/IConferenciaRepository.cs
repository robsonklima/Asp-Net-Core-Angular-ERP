using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IConferenciaRepository
    {
        PagedList<Conferencia> ObterPorParametros(ConferenciaParameters parameters);
        Conferencia ObterPorCodigo(int codigo);
        void Atualizar(Conferencia conferencia);
        void Criar(Conferencia conferencia);
        void Deletar(int codConferencia);
    }
}
