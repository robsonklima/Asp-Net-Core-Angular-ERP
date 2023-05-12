using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IOperadoraTelefoniaService
    {
        ListViewModel ObterPorParametros(OperadoraTelefoniaParameters parameters);
        OperadoraTelefonia Criar(OperadoraTelefonia op);
        OperadoraTelefonia Deletar(int codigo);
        OperadoraTelefonia Atualizar(OperadoraTelefonia op);
        OperadoraTelefonia ObterPorCodigo(int codigo);
    }
}
