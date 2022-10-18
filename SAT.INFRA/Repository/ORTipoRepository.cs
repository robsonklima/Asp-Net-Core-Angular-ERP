using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class ORTipoRepository : IORTipoRepository
    {
        private readonly AppDbContext _context;

        public ORTipoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ORTipo tipo)
        {
            _context.ChangeTracker.Clear();
            ORTipo p = _context.ORTipo.FirstOrDefault(p => p.CodTipoOR == tipo.CodTipoOR);

            if(p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(tipo);
                _context.SaveChanges();
            }
        }

        public void Criar(ORTipo tipo)
        {
            _context.Add(tipo);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            ORTipo ORTipo = _context.ORTipo.FirstOrDefault(p => p.CodTipoOR == codigo);

            if (ORTipo != null)
            {
                _context.ORTipo.Remove(ORTipo);
                _context.SaveChanges();
            }
        }

        public ORTipo ObterPorCodigo(int codigo)
        {
            return _context.ORTipo.FirstOrDefault(p => p.CodTipoOR == codigo);
        }

        public PagedList<ORTipo> ObterPorParametros(ORTipoParameters parameters)
        {
            var ORTipoes = _context.ORTipo.AsQueryable();

            if (parameters.Filter != null)
            {
                ORTipoes = ORTipoes.Where(
                    p =>
                    p.CodTipoOR.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                ORTipoes = ORTipoes.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<ORTipo>.ToPagedList(ORTipoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
