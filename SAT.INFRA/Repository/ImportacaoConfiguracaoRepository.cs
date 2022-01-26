using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public partial class ImportacaoConfiguracaoRepository : IImportacaoConfiguracaoRepository
    {
        private readonly AppDbContext _context;

        public ImportacaoConfiguracaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ImportacaoConfiguracao importacaoConf)
        {
            ImportacaoConfiguracao import = _context.ImportacaoConfiguracao.FirstOrDefault(i => i.CodImportacaoConf == importacaoConf.CodImportacaoConf);

            if (import != null)
            {
                _context.Entry(import).CurrentValues.SetValues(importacaoConf);
                _context.SaveChanges();
            }
        }

        public ImportacaoConfiguracao Criar(ImportacaoConfiguracao importacaoConf)
        {
            _context.Add(importacaoConf);
            _context.SaveChanges();

            return importacaoConf;
        }

        public void Deletar(int codigo)
        {
            ImportacaoConfiguracao import = _context.ImportacaoConfiguracao.FirstOrDefault(i => i.CodImportacaoConf == codigo);

            if (import != null)
            {
                _context.Remove(import);
                _context.SaveChanges();
            }
        }

        public ImportacaoConfiguracao ObterPorCodigo(int codigo)
        {
            return _context.ImportacaoConfiguracao
                               .Include(i => i.ImportacaoTipo)
                               .FirstOrDefault(i => i.CodImportacaoConf == codigo);
        }

        public PagedList<ImportacaoConfiguracao> ObterPorParametros(ImportacaoConfiguracaoParameters parameters)
        {
            var query = _context.ImportacaoConfiguracao
                               .Include(i => i.ImportacaoTipo)
                               .AsQueryable();

            if (parameters.CodImportacaoTipo is not null)
            {
                query = query.Where(i => i.CodImportacaoTipo == parameters.CodImportacaoTipo);
            }

            if (!string.IsNullOrEmpty(parameters.Titulo))
            {
                query = query.Where(i => i.Titulo.Contains(parameters.Titulo));

            }

            return PagedList<ImportacaoConfiguracao>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
