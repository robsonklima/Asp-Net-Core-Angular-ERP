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
    public class OSBancadaPecasRepository : IOSBancadaPecasRepository
    {
        private readonly AppDbContext _context;

        public OSBancadaPecasRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(OSBancadaPecas osBancada)
        {
            _context.ChangeTracker.Clear();
            OSBancadaPecas c = _context.OSBancadaPecas.FirstOrDefault(c => (c.CodOsbancada == osBancada.CodOsbancada) && (c.CodPecaRe5114 == osBancada.CodPecaRe5114) );

            if (c != null)
            {
                _context.Entry(c).CurrentValues.SetValues(osBancada);
                _context.Entry(c).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

         public OSBancadaPecas ObterPorCodigo(int codOsbancada, int codPecaRe5114)
        {
            return _context.OSBancadaPecas
                .Include(i => i.PecaRE5114)
                .Include(i => i.OSBancada)
                .SingleOrDefault(
                    i =>
                    i.CodOsbancada == codOsbancada &&
                    i.CodPecaRe5114 == codPecaRe5114 );
        }

        public void Criar(OSBancadaPecas osBancada)
        {
            _context.Add(osBancada);
            _context.SaveChanges();
        }

        public void Deletar(int codOSBancadaPecas, int codPecaRe5114)
        {
            OSBancadaPecas c = _context.OSBancadaPecas.FirstOrDefault(c => (c.CodOsbancada == codOSBancadaPecas) && (c.CodPecaRe5114 == codPecaRe5114) );

            if (c != null)
            {
                _context.OSBancadaPecas.Remove(c);
                _context.SaveChanges();
            }
        }
        public PagedList<OSBancadaPecas> ObterPorParametros(OSBancadaPecasParameters parameters)
        {
            IQueryable<OSBancadaPecas> osBancadaPecas = _context.OSBancadaPecas
                 .Include(i => i.OSBancada)
                     .ThenInclude(i => i.Filial)
                .Include(i => i.OSBancada)
                    .ThenInclude(i => i.ClienteBancada)
                .Include(i => i.PecaRE5114)
                    .ThenInclude(i => i.Peca)
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Filter))
                osBancadaPecas = osBancadaPecas.Where(
                    s =>
                    s.CodOsbancada.ToString().Contains(parameters.Filter) ||
                    s.PecaRE5114.NumRe5114.Contains(parameters.Filter) ||
                    s.OSBancada.ClienteBancada.Apelido.Contains(parameters.Filter) ||
                    s.OSBancada.ClienteBancada.NomeCliente.Contains(parameters.Filter) ||
                    s.OSBancada.Nfentrada.Contains(parameters.Filter)
             );

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                osBancadaPecas = osBancadaPecas.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodOsbancadas))
            {
                int[] cods = parameters.CodOsbancadas.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                osBancadaPecas = osBancadaPecas.Where(dc => cods.Contains(dc.OSBancada.CodOsbancada));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodPecaRe5114s))
            {
                int[] cods = parameters.CodPecaRe5114s.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                osBancadaPecas = osBancadaPecas.Where(dc => cods.Contains(dc.PecaRE5114.CodPecaRe5114));
            }

            return PagedList<OSBancadaPecas>.ToPagedList(osBancadaPecas, parameters.PageNumber, parameters.PageSize);
        }
    }
}
