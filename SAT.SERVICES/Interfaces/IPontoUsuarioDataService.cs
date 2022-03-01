using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IPontoUsuarioDataService
    {
        ListViewModel ObterPorParametros(PontoUsuarioDataParameters parameters);
        PontoUsuarioData Criar(PontoUsuarioData pontoUsuarioData);
        void Deletar(int codigo);
        void Atualizar(PontoUsuarioData pontoUsuarioData);
        PontoUsuarioData ObterPorCodigo(int codigo);
    }
}
