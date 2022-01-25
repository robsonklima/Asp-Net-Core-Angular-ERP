using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class PaisRepository : IPaisRepository
    {
        private readonly AppDbContext _context;

        public PaisRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Pais pais)
        {
            _context.ChangeTracker.Clear();
            Pais p = _context.Pais.FirstOrDefault(p => p.CodPais == pais.CodPais);

            if(p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(pais);
                _context.SaveChanges();
            }
        }

        public void Criar(Pais pais)
        {
            _context.Add(pais);
            _context.SaveChanges();
        }

        public void Deletar(int codPais)
        {
            Pais pais = _context.Pais.FirstOrDefault(p => p.CodPais == codPais);

            if (pais != null)
            {
                _context.Pais.Remove(pais);
                _context.SaveChanges();
            }
        }

        public Pais ObterPorCodigo(int codigo)
        {
            return _context.Pais.FirstOrDefault(p => p.CodPais == codigo);
        }

        public PagedList<Pais> ObterPorParametros(PaisParameters parameters)
        {
            var paises = _context.Pais.AsQueryable();

            if (parameters.Filter != null)
            {
                paises = paises.Where(
                            p =>
                            p.CodPais.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            p.NomePais.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                            
                );
            }

            if (parameters.CodPais != null)
            {
                paises = paises.Where(p => p.CodPais == parameters.CodPais);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                paises = paises.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Pais>.ToPagedList(paises, parameters.PageNumber, parameters.PageSize);
        }
    }
}
