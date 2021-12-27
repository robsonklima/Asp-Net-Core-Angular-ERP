using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IFotoRepository
    {
        void Criar(Foto foto);
        void Deletar(int codigo);
        Foto ObterPorCodigo(int codigo);
        public PagedList<Foto> ObterPorParametros(FotoParameters parameters);
    }
}
