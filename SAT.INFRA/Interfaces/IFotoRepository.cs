using SAT.MODELS.Entities;

namespace SAT.INFRA.Interfaces
{
    public interface IFotoRepository
    {
        void Criar(Foto foto);
        void Deletar(int codigo);
        Foto ObterPorCodigo(int codigo);
    }
}
