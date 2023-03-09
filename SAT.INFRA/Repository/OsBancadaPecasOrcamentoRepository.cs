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
    public class OsBancadaPecasOrcamentoRepository : IOsBancadaPecasOrcamentoRepository
    {
        private readonly AppDbContext _context;

        public OsBancadaPecasOrcamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(OsBancadaPecasOrcamento osBancadaPecasOrcamento)
        {
            _context.ChangeTracker.Clear();
            OsBancadaPecasOrcamento c = _context.OsBancadaPecasOrcamento.FirstOrDefault(c => c.CodOrcamento == osBancadaPecasOrcamento.CodOrcamento);

            if (c != null)
            {
                _context.Entry(c).CurrentValues.SetValues(osBancadaPecasOrcamento);
                _context.Entry(c).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void Criar(OsBancadaPecasOrcamento osBancadaPecasOrcamento)
        {
            _context.Add(osBancadaPecasOrcamento);
            _context.SaveChanges();
        }

        public void Deletar(int codOrcamento)
        {
            OsBancadaPecasOrcamento c = _context.OsBancadaPecasOrcamento.FirstOrDefault(c => c.CodOrcamento == codOrcamento);

            if (c != null)
            {
                _context.OsBancadaPecasOrcamento.Remove(c);
                _context.SaveChanges();
            }
        }

        public OsBancadaPecasOrcamento ObterPorCodigo(int codigo)
        {
            return _context.OsBancadaPecasOrcamento
                .Include(i => i.Usuario)
                .Include(i => i.OSBancadaPecas)
                    .ThenInclude(i => i.OSBancada.Filial)
                .Include(i => i.OSBancadaPecas)
                    .ThenInclude(i => i.OSBancada.ClienteBancada.Cidade.UnidadeFederativa)
                .Include(i => i.OSBancadaPecas)
                    .ThenInclude(i => i.PecaRE5114.Peca)
                .FirstOrDefault(c => c.CodOrcamento == codigo);
        }

        public PagedList<OsBancadaPecasOrcamento> ObterPorParametros(OsBancadaPecasOrcamentoParameters parameters)
        {
            IQueryable<OsBancadaPecasOrcamento> osBancadaPecasOrcamentos = _context.OsBancadaPecasOrcamento
                .Include(i => i.Usuario)
                .Include(i => i.OSBancadaPecas)
                    .ThenInclude(i => i.OSBancada.Filial)
                .Include(i => i.OSBancadaPecas)
                    .ThenInclude(i => i.OSBancada.ClienteBancada.Cidade.UnidadeFederativa)
                .Include(i => i.OSBancadaPecas)
                    .ThenInclude(i => i.PecaRE5114.Peca)
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Filter))
                osBancadaPecasOrcamentos = osBancadaPecasOrcamentos.Where(
                    s =>
                    s.CodOrcamento.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.OSBancadaPecas.OSBancada.CodOsbancada.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)||
                    s.OSBancadaPecas.PecaRE5114.NumRe5114.Contains(parameters.Filter) ||
                    s.OSBancadaPecas.PecaRE5114.Peca.CodMagnus.Contains(parameters.Filter) ||
                    s.OSBancadaPecas.OSBancada.ClienteBancada.Apelido.Contains(parameters.Filter) ||
                    s.OSBancadaPecas.OSBancada.ClienteBancada.NomeCliente.Contains(parameters.Filter) ||
                    s.OSBancadaPecas.OSBancada.Nfentrada.Contains(parameters.Filter)
             );
            if (parameters.CodOrcamento != null)
            {
                osBancadaPecasOrcamentos = osBancadaPecasOrcamentos.Where(a => a.CodOrcamento == parameters.CodOrcamento);
            };

            if (parameters.IndOrcamentoAprov != null)
            {
                osBancadaPecasOrcamentos = osBancadaPecasOrcamentos.Where(a => a.IndOrcamentoAprov == parameters.IndOrcamentoAprov);
            };

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                osBancadaPecasOrcamentos = osBancadaPecasOrcamentos.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodOSBancadas))
            {
                int[] cods = parameters.CodOSBancadas.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                osBancadaPecasOrcamentos = osBancadaPecasOrcamentos.Where(dc => cods.Contains(dc.OSBancadaPecas.OSBancada.CodOsbancada));
            }

            if (!string.IsNullOrWhiteSpace(parameters.NumRe5114))
            {
                string[] cods = parameters.NumRe5114.Split(",").Select(a => a.Trim()).Distinct().ToArray();
                osBancadaPecasOrcamentos = osBancadaPecasOrcamentos.Where(dc => cods.Contains(dc.OSBancadaPecas.PecaRE5114.NumRe5114));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodClienteBancadas))
            {
                int[] cods = parameters.CodClienteBancadas.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                osBancadaPecasOrcamentos = osBancadaPecasOrcamentos.Where(dc => cods.Contains(dc.OSBancadaPecas.OSBancada.ClienteBancada.CodClienteBancada));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodPecaRe5114s))
            {
                int[] cods = parameters.CodPecaRe5114s.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                osBancadaPecasOrcamentos = osBancadaPecasOrcamentos.Where(dc => cods.Contains(dc.OSBancadaPecas.PecaRE5114.CodPecaRe5114));
            }

            return PagedList<OsBancadaPecasOrcamento>.ToPagedList(osBancadaPecasOrcamentos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
