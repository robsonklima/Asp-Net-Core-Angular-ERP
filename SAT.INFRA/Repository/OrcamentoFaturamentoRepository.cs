using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class OrcamentoFaturamentoRepository : IOrcamentoFaturamentoRepository
    {
        private readonly AppDbContext _context;

        public OrcamentoFaturamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(OrcamentoFaturamento OrcamentoFaturamento)
        {
            _context.ChangeTracker.Clear();
            OrcamentoFaturamento p = _context.OrcamentoFaturamento.FirstOrDefault(p => p.CodOrcamentoFaturamento == OrcamentoFaturamento.CodOrcamentoFaturamento);

            if (p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(OrcamentoFaturamento);
                _context.SaveChanges();
            }
        }

        public void Criar(OrcamentoFaturamento OrcamentoFaturamento)
        {
            _context.Add(OrcamentoFaturamento);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            OrcamentoFaturamento OrcamentoFaturamento = _context
                .OrcamentoFaturamento
                .FirstOrDefault(p => p.CodOrcamentoFaturamento == codigo);

            if (OrcamentoFaturamento != null)
            {
                _context.OrcamentoFaturamento.Remove(OrcamentoFaturamento);
                _context.SaveChanges();
            }
        }

        public OrcamentoFaturamento ObterPorCodigo(int codigo)
        {
            return _context.OrcamentoFaturamento
                .FirstOrDefault(p => p.CodOrcamentoFaturamento == codigo);
        }

        public PagedList<OrcamentoFaturamento> ObterPorParametros(OrcamentoFaturamentoParameters parameters)
        {
            var query = _context.OrcamentoFaturamento
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(p =>
                    p.CodOrcamento.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.NumOrcamento.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.NumNF != null)
            {
                query = query.Where(p => p.NumNF == parameters.NumNF);
            }

            if (parameters.DescricaoNotaFiscal != null)
            {
                query = query.Where(p => p.DescricaoNotaFiscal == parameters.DescricaoNotaFiscal);
            }            

            if (parameters.DataEmissaoNF != null)
            {
                query = query.Where(p => p.DataEmissaoNF == parameters.DataEmissaoNF);
            }  

            if (parameters.IndFaturado != null)
            {
                query = query.Where(p => p.IndFaturado == parameters.IndFaturado);
            }              

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<OrcamentoFaturamento>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
