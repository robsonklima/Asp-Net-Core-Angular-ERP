using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repositories
{
    public class FeriadoRepository : IFeriadoRepository
    {
        private readonly AppDbContext _context;

        public FeriadoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Feriado feriado)
        {
            Feriado f = _context.Feriado.FirstOrDefault(f => f.CodFeriado == feriado.CodFeriado);

            if (f != null)
            {
                _context.Entry(f).CurrentValues.SetValues(feriado);
                _context.SaveChanges();
            }
        }

        public void Criar(Feriado feriado)
        {
            _context.Add(feriado);
            _context.SaveChanges();
        }

        public void Deletar(int codFeriado)
        {
            Feriado f = _context.Feriado.FirstOrDefault(f => f.CodFeriado == codFeriado);

            if (f != null)
            {
                _context.Feriado.Remove(f);
                _context.SaveChanges();
            }
        }

        public Feriado ObterPorCodigo(int codigo)
        {
            return _context.Feriado.FirstOrDefault(f => f.CodFeriado == codigo);
        }

        public PagedList<Feriado> ObterPorParametros(FeriadoParameters parameters)
        {
            var feriados = _context.Feriado
                .Include(f => f.Cidade)
                .Include(f => f.UnidadeFederativa)
                .Include(f => f.Pais)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                feriados = feriados.Where(
                            f =>
                            f.CodFeriado.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            f.NomeFeriado.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodFeriado != null)
            {
                feriados = feriados.Where(f => f.CodFeriado == parameters.CodFeriado);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                feriados = feriados.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Feriado>.ToPagedList(feriados, parameters.PageNumber, parameters.PageSize);
        }
    }
}
