using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IPontoUsuarioDataDivergenciaRepository
    {
        PagedList<PontoUsuarioDataDivergencia> ObterPorParametros(PontoUsuarioDataDivergenciaParameters parameters);
        void Criar(PontoUsuarioDataDivergencia pontoUsuarioDataDivergencia);
        void Deletar(int codigo);
        void Atualizar(PontoUsuarioDataDivergencia pontoUsuarioDataDivergencia);
        PontoUsuarioDataDivergencia ObterPorCodigo(int codigo);
    }
}