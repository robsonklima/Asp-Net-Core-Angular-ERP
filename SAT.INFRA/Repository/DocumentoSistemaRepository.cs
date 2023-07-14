using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public partial class DocumentoSistemaRepository : IDocumentoSistemaRepository
    {
        private readonly AppDbContext _context;

        public DocumentoSistemaRepository(AppDbContext context)
        {
            _context = context;
        }

        public DocumentoSistema Atualizar(DocumentoSistema doc)
        {
            try
            {
                _context.ChangeTracker.Clear();
                DocumentoSistema linha = _context.DocumentoSistema.FirstOrDefault(c => c.CodDocumentoSistema == doc.CodDocumentoSistema);

                if (linha != null)
                {
                    _context.Entry(linha).CurrentValues.SetValues(doc);
                    _context.Entry(linha).State = EntityState.Modified;
                    _context.SaveChanges();
                }

                return doc;
            }
            catch
            {
                throw;
            }
        }

        public DocumentoSistema Criar(DocumentoSistema documentosistema)
        {
            _context.Add(documentosistema);

            _context.SaveChanges();

            return documentosistema;
        }

        public DocumentoSistema Deletar(int cod)
        {
            DocumentoSistema linha = _context.DocumentoSistema.FirstOrDefault(c => c.CodDocumentoSistema == cod);

            if (linha != null)
            {
                _context.DocumentoSistema.Remove(linha);
                _context.SaveChanges();
            }

            return linha;
        }

        public DocumentoSistema ObterPorCodigo(int codigo)
        {
            return _context.DocumentoSistema
                .FirstOrDefault(f => f.CodDocumentoSistema == codigo);
        }

        public PagedList<DocumentoSistema> ObterPorParametros(DocumentoSistemaParameters parameters)
        {
            var query = _context.DocumentoSistema
               .AsQueryable();

            return PagedList<DocumentoSistema>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
