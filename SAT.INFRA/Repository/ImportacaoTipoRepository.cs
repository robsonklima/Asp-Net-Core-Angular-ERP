using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public partial class ImportacaoTipoRepository : IImportacaoTipoRepository
    {
        private readonly AppDbContext _context;

        public ImportacaoTipoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ImportacaoTipo importacaoTipo )
        {
            ImportacaoTipo import = _context.ImportacaoTipo.FirstOrDefault(i => i.CodImportacaoTipo == importacaoTipo.CodImportacaoTipo);

            if (import != null)
            {
                _context.Entry(import).CurrentValues.SetValues(importacaoTipo);
                _context.SaveChanges();
            }
        }

        public ImportacaoTipo Criar(ImportacaoTipo importacaoTipo)
        {
            _context.Add(importacaoTipo);
            _context.SaveChanges();

            return importacaoTipo;
        }

        public void Deletar(int codigo)
        {
            ImportacaoTipo import = _context.ImportacaoTipo.FirstOrDefault(i => i.CodImportacaoTipo == codigo);

            if (import != null)
            {
                _context.Remove(import);
                _context.SaveChanges();
            }
        }

        public ImportacaoTipo ObterPorCodigo(int codigo)
        {
            return _context.ImportacaoTipo.FirstOrDefault(i => i.CodImportacaoTipo == codigo);
        }

        public PagedList<ImportacaoTipo> ObterPorParametros(ImportacaoTipoParameters parameters)
        {
            var query = _context.ImportacaoTipo.AsQueryable();

            if (parameters.CodImportacaoTipo is not null)
            {
                query = query.Where(i => i.CodImportacaoTipo == parameters.CodImportacaoTipo);
            } 
            
            if (parameters.IndAtivo is not null)
            {
                query = query.Where(i => i.IndAtivo == parameters.IndAtivo);
            }

            if (!string.IsNullOrEmpty(parameters.NomeTipo))
            {
                query = query.Where(i => i.NomeTipo.Contains(parameters.NomeTipo));
            }

            if (parameters.IndAtivo.HasValue)
            {
                query = query.Where(i => i.IndAtivo ==  parameters.IndAtivo.Value);
            }

            if (!string.IsNullOrWhiteSpace(parameters.SortActive) && !string.IsNullOrWhiteSpace(parameters.SortDirection))
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<ImportacaoTipo>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
