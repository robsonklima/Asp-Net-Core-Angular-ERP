using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IFotoService
    {
        Foto Criar(Foto foto);
        void Deletar(int codigo);
        Foto ObterPorCodigo(int codigo);
    }
}
