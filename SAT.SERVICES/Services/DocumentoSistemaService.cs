using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DocumentoSistemaService : IDocumentoSistemaService
    {
        private readonly IDocumentoSistemaRepository _documentosistemaRepo;

        public DocumentoSistemaService(IDocumentoSistemaRepository documentosistemaRepo)
        {
            _documentosistemaRepo = documentosistemaRepo;
        }

        public ListViewModel ObterPorParametros(DocumentoSistemaParameters parameters)
        {
            var filiais = _documentosistemaRepo.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = filiais,
                TotalCount = filiais.TotalCount,
                CurrentPage = filiais.CurrentPage,
                PageSize = filiais.PageSize,
                TotalPages = filiais.TotalPages,
                HasNext = filiais.HasNext,
                HasPrevious = filiais.HasPrevious
            };
        }

        public DocumentoSistema Criar(DocumentoSistema documentosistema)
        {
            _documentosistemaRepo.Criar(documentosistema);

            return documentosistema;
        }

        public DocumentoSistema Deletar(int codigo)
        {
            return _documentosistemaRepo.Deletar(codigo);
        }

        public DocumentoSistema Atualizar(DocumentoSistema documentosistema)
        {
            return _documentosistemaRepo.Atualizar(documentosistema);
        }

        public DocumentoSistema ObterPorCodigo(int codigo)
        {
            return _documentosistemaRepo.ObterPorCodigo(codigo);
        }
    }
}
