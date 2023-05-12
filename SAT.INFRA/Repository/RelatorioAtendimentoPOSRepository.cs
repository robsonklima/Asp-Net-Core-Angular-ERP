using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class RelatorioAtendimentoPOSRepository : IRelatorioAtendimentoPOSRepository
    {
        private readonly AppDbContext _context;

        public RelatorioAtendimentoPOSRepository(AppDbContext context)
        {
            _context = context;
        }

        public RelatorioAtendimentoPOS Atualizar(RelatorioAtendimentoPOS relatorio)
        {
            _context.ChangeTracker.Clear();
            RelatorioAtendimentoPOS rat = _context.RelatorioAtendimentoPOS.FirstOrDefault(rat => rat.CodRATbanrisul == relatorio.CodRATbanrisul);

            if (rat != null)
            {
                _context.Entry(rat).CurrentValues.SetValues(relatorio);
                _context.SaveChanges();
            }

            return relatorio;
        }

        public RelatorioAtendimentoPOS Criar(RelatorioAtendimentoPOS relatorio)
        {
            _context.Add(relatorio);
            _context.SaveChanges();
            return relatorio;
        }

        public RelatorioAtendimentoPOS Deletar(int codRATbanrisul)
        {
            RelatorioAtendimentoPOS rat = _context.RelatorioAtendimentoPOS.FirstOrDefault(rat => rat.CodRATbanrisul == codRATbanrisul);

            if (rat != null)
            {
                _context.RelatorioAtendimentoPOS.Remove(rat);
                _context.SaveChanges();
            }

            return rat;
        }

        public RelatorioAtendimentoPOS ObterPorCodigo(int codRATbanrisul)
        {
            var relatorio = _context.RelatorioAtendimentoPOS;

            return relatorio.FirstOrDefault(rat => rat.CodRATbanrisul == codRATbanrisul);
        }

        public PagedList<RelatorioAtendimentoPOS> ObterPorParametros(RelatorioAtendimentoPOSParameters parameters)
        {
            var query = _context.RelatorioAtendimentoPOS.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<RelatorioAtendimentoPOS>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}