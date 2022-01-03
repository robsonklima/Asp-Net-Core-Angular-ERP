using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class OrcamentoRepository : IOrcamentoRepository
    {
        private readonly AppDbContext _context;

        public OrcamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Orcamento orcamento)
        {
            Orcamento p = _context.Orcamento.FirstOrDefault(p => p.CodOrc == orcamento.CodOrc);

            if (p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(orcamento);
                _context.SaveChanges();
            }
        }

        public void Criar(Orcamento orcamento)
        {
            _context.Add(orcamento);
            _context.SaveChanges();
        }

        public void Deletar(int codOrc)
        {
            Orcamento orcamento = _context.Orcamento.FirstOrDefault(p => p.CodOrc == codOrc);

            if (orcamento != null)
            {
                _context.Orcamento.Remove(orcamento);
                _context.SaveChanges();
            }
        }

        public Orcamento ObterPorCodigo(int codigo) =>
            _context.Orcamento
                .Include(p => p.EnderecoFaturamentoNF)
                    .ThenInclude(p => p.CidadeEnvioNF)
                        .ThenInclude(p => p.UnidadeFederativa)
                .Include(p => p.EnderecoFaturamentoNF)
                    .ThenInclude(p => p.CidadeFaturamento)
                        .ThenInclude(p => p.UnidadeFederativa)
                .FirstOrDefault(p => p.CodOrc == codigo);

        public PagedList<Orcamento> ObterPorParametros(OrcamentoParameters parameters)
        {
            var orcamentoes = _context.Orcamento.AsQueryable();

            if (parameters.Filter != null)
            {
                orcamentoes = orcamentoes.Where(p =>
                    p.CodOrc.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodOrc != null)
            {
                orcamentoes = orcamentoes.Where(n => n.CodOrc == parameters.CodOrc);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                orcamentoes = orcamentoes.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Orcamento>.ToPagedList(orcamentoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
