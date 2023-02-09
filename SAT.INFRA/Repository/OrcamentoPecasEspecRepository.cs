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
    public class OrcamentoPecasEspecRepository : IOrcamentoPecasEspecRepository
    {
        private readonly AppDbContext _context;

        public OrcamentoPecasEspecRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(OrcamentoPecasEspec orcamentoPecasEspec)
        {
            _context.ChangeTracker.Clear();
            OrcamentoPecasEspec c = _context.OrcamentoPecasEspec.FirstOrDefault(c => c.CodOrcamentoPecasEspec == orcamentoPecasEspec.CodOrcamentoPecasEspec);

            if (c != null)
            {
                _context.Entry(c).CurrentValues.SetValues(orcamentoPecasEspec);
                _context.Entry(c).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void Criar(OrcamentoPecasEspec orcamentoPecasEspec)
        {
            _context.Add(orcamentoPecasEspec);
            _context.SaveChanges();
        }

        public void Deletar(int codOrcamentoPecasEspec)
        {
            OrcamentoPecasEspec c = _context.OrcamentoPecasEspec.FirstOrDefault(c => c.CodOrcamentoPecasEspec == codOrcamentoPecasEspec);

            if (c != null)
            {
                _context.OrcamentoPecasEspec.Remove(c);
                _context.SaveChanges();
            }
        }

        public OrcamentoPecasEspec ObterPorCodigo(int codigo)
        {
            return _context.OrcamentoPecasEspec
                .Include(i => i.OSBancada)
                .Include(i => i.OSBancadaPecasOrcamento)
                .Include(i => i.PecaRE5114)
                .Include(i => i.Peca)
                .FirstOrDefault(c => c.CodOrcamentoPecasEspec == codigo);
        }

        public PagedList<OrcamentoPecasEspec> ObterPorParametros(OrcamentoPecasEspecParameters parameters)
        {
            IQueryable<OrcamentoPecasEspec> orcamentoPecasEspecs = _context.OrcamentoPecasEspec
                .Include(i => i.OSBancada)
                .Include(i => i.OSBancadaPecasOrcamento)
                .Include(i => i.PecaRE5114)
                .Include(i => i.Peca)
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Filter))
                orcamentoPecasEspecs = orcamentoPecasEspecs.Where(
                    s =>
                    s.CodOrcamentoPecasEspec.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.OSBancada.CodOsbancada.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.OSBancadaPecasOrcamento.CodOrcamento.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.PecaRE5114.CodPecaRe5114.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
             );

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                orcamentoPecasEspecs = orcamentoPecasEspecs.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodOrcamentos))
            {
                int[] cods = parameters.CodOrcamentos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                orcamentoPecasEspecs = orcamentoPecasEspecs.Where(dc => cods.Contains(dc.OSBancadaPecasOrcamento.CodOrcamento));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodOsbancadas))
            {
                int[] cods = parameters.CodOsbancadas.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                orcamentoPecasEspecs = orcamentoPecasEspecs.Where(dc => cods.Contains(dc.OSBancada.CodOsbancada));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodPecaRe5114s))
            {
                int[] cods = parameters.CodPecaRe5114s.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                orcamentoPecasEspecs = orcamentoPecasEspecs.Where(dc => cods.Contains(dc.PecaRE5114.CodPecaRe5114));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodPecas))
            {
                int[] cods = parameters.CodPecas.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                orcamentoPecasEspecs = orcamentoPecasEspecs.Where(dc => cods.Contains(dc.Peca.CodPeca));
            }

            return PagedList<OrcamentoPecasEspec>.ToPagedList(orcamentoPecasEspecs, parameters.PageNumber, parameters.PageSize);
        }
    }
}
