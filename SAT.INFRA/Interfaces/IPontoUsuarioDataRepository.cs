using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IPontoUsuarioDataRepository
    {
        PagedList<PontoUsuarioData> ObterPorParametros(PontoUsuarioDataParameters parameters);
        void Criar(PontoUsuarioData pontoUsuarioData);
        void Deletar(int codigo);
        void Atualizar(PontoUsuarioData pontoUsuarioData);
        PontoUsuarioData ObterPorCodigo(int codigo);
    }
}