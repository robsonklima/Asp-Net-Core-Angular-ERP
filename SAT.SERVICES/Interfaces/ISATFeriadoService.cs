using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ISATFeriadoService
    {
        ListViewModel ObterPorParametros(SATFeriadoParameters parameters);
        SATFeriado Criar(SATFeriado feriado);
        void Deletar(int codigo);
        void Atualizar(SATFeriado feriado);
        SATFeriado ObterPorCodigo(int codigo);
    }
}
