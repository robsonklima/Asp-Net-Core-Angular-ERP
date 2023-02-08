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
    public class OSBancadaRepository : IOSBancadaRepository
    {
        private readonly AppDbContext _context;

        public OSBancadaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(OSBancada osBancada)
        {
            _context.ChangeTracker.Clear();
            OSBancada c = _context.OSBancada.FirstOrDefault(c => c.CodOsbancada == osBancada.CodOsbancada);

            if (c != null)
            {
                _context.Entry(c).CurrentValues.SetValues(osBancada);
                _context.Entry(c).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void Criar(OSBancada osBancada)
        {
            _context.Add(osBancada);
            _context.SaveChanges();
        }

        public void Deletar(int codOSBancada)
        {
            OSBancada c = _context.OSBancada.FirstOrDefault(c => c.CodOsbancada == codOSBancada);

            if (c != null)
            {
                _context.OSBancada.Remove(c);
                _context.SaveChanges();
            }
        }

        public OSBancada ObterPorCodigo(int codigo)
        {
            return _context.OSBancada
                .Include(i => i.Filial)
                .Include(i => i.ClienteBancada)
                .FirstOrDefault(c => c.CodClienteBancada == codigo);
        }

        public PagedList<OSBancada> ObterPorParametros(OSBancadaParameters parameters)
        {
            IQueryable<OSBancada> osBancadas = _context.OSBancada
                .Include(i => i.Filial)
                .Include(i => i.ClienteBancada)
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Filter))
                osBancadas = osBancadas.Where(
                    s =>
                    s.CodOsbancada.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.Filial.NomeFilial.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.ClienteBancada.NomeCliente.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
             );

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                osBancadas = osBancadas.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodFiliais))
            {
                int[] cods = parameters.CodFiliais.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                osBancadas = osBancadas.Where(dc => cods.Contains(dc.Filial.CodFilial));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodClienteBancadas))
            {
                int[] cods = parameters.CodClienteBancadas.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                osBancadas = osBancadas.Where(dc => cods.Contains(dc.CodClienteBancada));
            }

            return PagedList<OSBancada>.ToPagedList(osBancadas, parameters.PageNumber, parameters.PageSize);
        }
    }
}
