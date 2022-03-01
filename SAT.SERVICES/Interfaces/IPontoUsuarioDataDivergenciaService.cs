using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IPontoUsuarioDataDivergenciaService
    {
        ListViewModel ObterPorParametros(PontoUsuarioDataDivergenciaParameters parameters);
        PontoUsuarioDataDivergencia Criar(PontoUsuarioDataDivergencia pontoUsuarioDataDivergencia);
        void Deletar(int codigo);
        void Atualizar(PontoUsuarioDataDivergencia pontoUsuarioDataDivergencia);
        PontoUsuarioDataDivergencia ObterPorCodigo(int codigo);
    }
}
