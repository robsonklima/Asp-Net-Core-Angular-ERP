using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface IArquivoBanrisulService
    {
        ListViewModel ObterPorParametros(ArquivoBanrisulParameters parameters);
        ArquivoBanrisul ObterPorCodigo(int codigo);
        void Criar(ArquivoBanrisul arquivo);
        void Deletar(int codigo);
        void Atualizar(ArquivoBanrisul arquivo);
    }
}