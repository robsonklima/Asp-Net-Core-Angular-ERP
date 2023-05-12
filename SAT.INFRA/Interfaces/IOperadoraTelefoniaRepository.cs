using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IOperadoraTelefoniaRepository
    {
        PagedList<OperadoraTelefonia> ObterPorParametros(OperadoraTelefoniaParameters parameters);
        OperadoraTelefonia Criar(OperadoraTelefonia op);
        OperadoraTelefonia Deletar(int codigo);
        OperadoraTelefonia Atualizar(OperadoraTelefonia op);
        OperadoraTelefonia ObterPorCodigo(int codigo);
    }
}
