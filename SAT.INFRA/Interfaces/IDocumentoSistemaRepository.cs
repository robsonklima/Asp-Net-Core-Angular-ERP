using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDocumentoSistemaRepository
    {
        PagedList<DocumentoSistema> ObterPorParametros(DocumentoSistemaParameters data);
        DocumentoSistema Criar(DocumentoSistema data);
        DocumentoSistema Deletar(int codigo);
        DocumentoSistema Atualizar(DocumentoSistema data);
        DocumentoSistema ObterPorCodigo(int codigo);
    }
}
