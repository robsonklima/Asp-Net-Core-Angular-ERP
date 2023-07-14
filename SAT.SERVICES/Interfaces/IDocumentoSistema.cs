using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDocumentoSistemaService
    {
        ListViewModel ObterPorParametros(DocumentoSistemaParameters data);
        DocumentoSistema Criar(DocumentoSistema data);
        DocumentoSistema Deletar(int codigo);
        DocumentoSistema Atualizar(DocumentoSistema data);
        DocumentoSistema ObterPorCodigo(int codigo);
    }
}
