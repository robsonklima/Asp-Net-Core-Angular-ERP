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
    public class FerramentaTecnicoRepository : IFerramentaTecnicoRepository
    {
        private readonly AppDbContext _context;

        public FerramentaTecnicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(FerramentaTecnico ferramentaTecnico)
        {
            _context.ChangeTracker.Clear();
            FerramentaTecnico c = _context.FerramentaTecnico.FirstOrDefault(c => c.CodFerramentaTecnico == ferramentaTecnico.CodFerramentaTecnico);

            if (c != null)
            {
                _context.Entry(c).CurrentValues.SetValues(ferramentaTecnico);
                _context.Entry(c).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void Criar(FerramentaTecnico ferramentaTecnico)
        {
            _context.Add(ferramentaTecnico);
            _context.SaveChanges();
        }

        public void Deletar(int codFerramentaTecnico)
        {
            FerramentaTecnico c = _context.FerramentaTecnico.FirstOrDefault(c => c.CodFerramentaTecnico == codFerramentaTecnico);

            if (c != null)
            {
                _context.FerramentaTecnico.Remove(c);
                _context.SaveChanges();
            }
        }

        public FerramentaTecnico ObterPorCodigo(int codigo)
        {
            return _context.FerramentaTecnico.FirstOrDefault(c => c.CodFerramentaTecnico == codigo);
        }

        public PagedList<FerramentaTecnico> ObterPorParametros(FerramentaTecnicoParameters parameters)
        {
            IQueryable<FerramentaTecnico> ferrTecnicos = _context.FerramentaTecnico.AsQueryable();

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                ferrTecnicos = ferrTecnicos.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<FerramentaTecnico>.ToPagedList(ferrTecnicos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
