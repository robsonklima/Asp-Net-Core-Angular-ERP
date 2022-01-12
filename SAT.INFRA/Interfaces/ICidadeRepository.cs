using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ICidadeRepository
    {
        PagedList<Cidade> ObterPorParametros(CidadeParameters parameters);
        void Criar(Cidade cidade);
        void Atualizar(Cidade cidade);
        void Deletar(int codCidade);
        Cidade ObterPorCodigo(int codigo);
        Cidade BuscaCidadePorNome(string nomeCidade);
    }
}
