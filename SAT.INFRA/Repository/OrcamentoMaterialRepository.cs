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
    public class OrcamentoMaterialRepository : IOrcamentoMaterialRepository
    {
        private readonly AppDbContext _context;

        public OrcamentoMaterialRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(OrcamentoMaterial orcamentoMaterial)
        {
            _context.ChangeTracker.Clear();
            OrcamentoMaterial p = _context.OrcamentoMaterial.FirstOrDefault(p => p.CodOrcMaterial == orcamentoMaterial.CodOrcMaterial);

            if (p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(orcamentoMaterial);
                _context.SaveChanges();
            }
        }

        public void Criar(OrcamentoMaterial orcamentoMaterial)
        {
            _context.Add(orcamentoMaterial);
            _context.SaveChanges();
        }

        public void Deletar(int codOrcMaterial)
        {
            OrcamentoMaterial orcamentoMat = _context.OrcamentoMaterial.FirstOrDefault(p => p.CodOrcMaterial == codOrcMaterial);

            if (orcamentoMat != null)
            {
                _context.OrcamentoMaterial.Remove(orcamentoMat);
                _context.SaveChanges();
            }
        }

        public OrcamentoMaterial ObterPorCodigo(int codigo)
        {
            return _context.OrcamentoMaterial
                .Include(o => o.Peca)
                .FirstOrDefault(p => p.CodOrcMaterial == codigo);
        }

        public PagedList<OrcamentoMaterial> ObterPorParametros(OrcamentoMaterialParameters parameters)
        {
            var query = _context.OrcamentoMaterial
                .Include(o => o.Peca)
                .AsQueryable();

            if (parameters.CodOrc != null)
            {
                query = query.Where(p => p.CodOrc == parameters.CodOrc);
            }

            if (parameters.CodigoPeca != null)
            {
                query = query.Where(p => p.CodigoPeca == parameters.CodigoPeca);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<OrcamentoMaterial>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
