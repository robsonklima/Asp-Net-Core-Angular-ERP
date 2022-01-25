using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class DefeitoRepository : IDefeitoRepository
    {
        private readonly AppDbContext _context;

        public DefeitoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Defeito defeito)
        {
            _context.ChangeTracker.Clear();
            Defeito d = _context.Defeito.FirstOrDefault(d => d.CodDefeito == defeito.CodDefeito);

            if (d != null)
            {
                _context.Entry(d).CurrentValues.SetValues(defeito);
                _context.SaveChanges();
            }
        }

        public void Criar(Defeito defeito)
        {
            _context.Add(defeito);
            _context.SaveChanges();
        }

        public void Deletar(int codDefeito)
        {
            Defeito d = _context.Defeito.FirstOrDefault(d => d.CodDefeito == codDefeito);

            if (d != null)
            {
                _context.Defeito.Remove(d);
                _context.SaveChanges();
            }
        }

        public Defeito ObterPorCodigo(int codigo)
        {
            return _context.Defeito.FirstOrDefault(d => d.CodDefeito == codigo);
        }

        public PagedList<Defeito> ObterPorParametros(DefeitoParameters parameters)
        {
            var defeitos = _context.Defeito.AsQueryable();

            if (parameters.Filter != null)
            {
                defeitos = defeitos.Where(
                    s =>
                    s.CodDefeito.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.CodEDefeito.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.NomeDefeito.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodDefeito != null)
            {
                defeitos = defeitos.Where(c => c.CodDefeito == parameters.CodDefeito);
            }

            if (parameters.IndAtivo != null)
            {
                defeitos = defeitos.Where(c => c.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                defeitos = defeitos.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Defeito>.ToPagedList(defeitos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
