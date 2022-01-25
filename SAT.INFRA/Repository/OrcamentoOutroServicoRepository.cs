using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class OrcamentoOutroServicoRepository : IOrcamentoOutroServicoRepository
    {
        private readonly AppDbContext _context;

        public OrcamentoOutroServicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(OrcamentoOutroServico orcOutrServ)
        {
            OrcamentoOutroServico p = _context.OrcamentoOutroServico.FirstOrDefault(p => p.CodOrcOutroServico == orcOutrServ.CodOrcOutroServico);

            if (p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(orcOutrServ);
                _context.SaveChanges();
            }
        }

        public void Criar(OrcamentoOutroServico orcOutrServ)
        {
            _context.Add(orcOutrServ);
            _context.SaveChanges();
        }

        public void Deletar(int codOrcOutroServico)
        {
            OrcamentoOutroServico orcamento = _context.OrcamentoOutroServico.FirstOrDefault(p => p.CodOrcOutroServico == codOrcOutroServico);

            if (orcamento != null)
            {
                _context.OrcamentoOutroServico.Remove(orcamento);
                _context.SaveChanges();
            }
        }

        public OrcamentoOutroServico ObterPorCodigo(int codigo)
        {
            return _context.OrcamentoOutroServico
                .FirstOrDefault(p => p.CodOrc == codigo);
        }

        public PagedList<OrcamentoOutroServico> ObterPorParametros(OrcamentoOutroServicoParameters parameters)
        {
            var query = _context.OrcamentoOutroServico
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Tipo))
            {
                query = query.Where(orc => orc.Tipo.Contains(parameters.Tipo));
            }

            if (parameters.CodOrc != null)
            {
                query = query.Where(orc => orc.CodOrc == parameters.CodOrc);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<OrcamentoOutroServico>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
