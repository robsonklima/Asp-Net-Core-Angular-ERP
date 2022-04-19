using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Context;

namespace SAT.INFRA.Repository
{
    public class TipoServicoRepository : ITipoServicoRepository
    {
        private readonly AppDbContext _context;

        public TipoServicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(TipoServico tipoServico)
        {
            _context.ChangeTracker.Clear();
            TipoServico ts = _context.TipoServico.FirstOrDefault(ts => ts.CodServico == tipoServico.CodServico);

            if (ts != null)
            {
                _context.Entry(ts).CurrentValues.SetValues(tipoServico);
                _context.SaveChanges();
            }
        }

        public void Criar(TipoServico tipoServico)
        {
            _context.Add(tipoServico);
            _context.SaveChanges();
        }

        public void Deletar(int codServico)
        {
            TipoServico ts = _context.TipoServico.FirstOrDefault(ts => ts.CodServico == codServico);

            if (ts != null)
            {
                _context.TipoServico.Remove(ts);
                _context.SaveChanges();
            }
        }

        public TipoServico ObterPorCodigo(int codigo)
        {
            return _context.TipoServico.FirstOrDefault(ts => ts.CodServico == codigo);
        }

        public PagedList<TipoServico> ObterPorParametros(TipoServicoParameters parameters)
        {
            var tipos = _context.TipoServico.AsQueryable();

            if (parameters.Filter != null)
            {
                tipos = tipos.Where(
                            t =>
                            t.NomeServico.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            t.CodETipoServico.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            t.CodServico.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodServico != null)
            {
                tipos = tipos.Where(t => t.CodServico == parameters.CodServico);
            }

            if (parameters.IndAtivo != null)
            {
                tipos = tipos.Where(t => t.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                tipos = tipos.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<TipoServico>.ToPagedList(tipos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
