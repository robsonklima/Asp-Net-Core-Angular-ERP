using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ICidadeService
    {
        ListViewModel ObterPorParametros(CidadeParameters parameters);
        Cidade Criar(Cidade cidade);
        void Deletar(int codigo);
        void Atualizar(Cidade cidade);
        Cidade ObterPorCodigo(int codigo);
    }
}
