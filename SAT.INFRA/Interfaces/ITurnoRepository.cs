using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ITurnoRepository
    {
        PagedList<Turno> ObterPorParametros(TurnoParameters parameters);
        void Criar(Turno turno);
        void Atualizar(Turno turno);
        void Deletar(int codTurno);
        Turno ObterPorCodigo(int codigo);
    }
}
