using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces 
{
    public interface IArquivoBanrisulRepository 
    {
        PagedList<ArquivoBanrisul> ObterPorParametros(ArquivoBanrisulParameters parameters);
        ArquivoBanrisul ObterPorCodigo(int codigo);
        void Criar(ArquivoBanrisul auditoria);
        void Deletar(int codigo);
        void Atualizar(ArquivoBanrisul arquivo);
    }
}