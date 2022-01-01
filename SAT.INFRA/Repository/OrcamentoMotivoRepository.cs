using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class OrcamentoMotivoRepository : IOrcamentoMotivoRepository
    {
        private readonly AppDbContext _context;

        public OrcamentoMotivoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(OrcamentoMotivo motivo)
        {
            OrcamentoMotivo p = _context.OrcamentoMotivo.FirstOrDefault(p => p.CodOrcMotivo == motivo.CodOrcMotivo);

            if(p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(motivo);
                _context.SaveChanges();
            }
        }

        public void Criar(OrcamentoMotivo motivo)
        {
            _context.Add(motivo);
            _context.SaveChanges();
        }

        public void Deletar(int codOrdcMotivo)
        {
            OrcamentoMotivo motivo = _context.OrcamentoMotivo.FirstOrDefault(p => p.CodOrcMotivo == codOrdcMotivo);

            if (motivo != null)
            {
                _context.OrcamentoMotivo.Remove(motivo);
                _context.SaveChanges();
            }
        }

        public OrcamentoMotivo ObterPorCodigo(int codigo)
        {
            return _context.OrcamentoMotivo.FirstOrDefault(p => p.CodOrcMotivo == codigo);
        }

        public PagedList<OrcamentoMotivo> ObterPorParametros(OrcamentoMotivoParameters parameters)
        {
            var motivoes = _context.OrcamentoMotivo.AsQueryable();

            if (parameters.Filter != null)
            {
                motivoes = motivoes.Where(p =>
                    p.CodOrcMotivo.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.Descricao.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                motivoes = motivoes.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<OrcamentoMotivo>.ToPagedList(motivoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
