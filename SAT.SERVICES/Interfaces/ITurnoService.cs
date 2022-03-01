using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ITurnoService
    {
        ListViewModel ObterPorParametros(TurnoParameters parameters);
        Turno Criar(Turno turno);
        void Deletar(int codigo);
        void Atualizar(Turno turno);
        Turno ObterPorCodigo(int codigo);
    }
}
