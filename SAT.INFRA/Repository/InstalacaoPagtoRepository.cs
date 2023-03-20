using SAT.INFRA.Context;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public partial class InstalacaoPagtoRepository : IInstalacaoPagtoRepository
    {
        private readonly AppDbContext _context;

        public InstalacaoPagtoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(InstalacaoPagto instalacaoPagto)
        {
            _context.ChangeTracker.Clear();
            InstalacaoPagto inst = _context.InstalacaoPagto.FirstOrDefault(i => i.CodInstalPagto == instalacaoPagto.CodInstalPagto);

            if (inst != null)
            {
                _context.Entry(inst).CurrentValues.SetValues(instalacaoPagto);
                _context.SaveChanges();
            }
        }

        public void Criar(InstalacaoPagto instalacaoPagto)
        {
            _context.Add(instalacaoPagto);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            InstalacaoPagto inst = _context.InstalacaoPagto.FirstOrDefault(i => i.CodInstalPagto == codigo);

            if (inst != null)
            {
                _context.InstalacaoPagto.Remove(inst);
                _context.SaveChanges();
            }
        }

        public InstalacaoPagto ObterPorCodigo(int codigo)
        {
            return _context.InstalacaoPagto
                .Include(i => i.Contrato)
                .Include(i => i.InstalacoesPagtoInstal) 
                     .ThenInclude(c => c.Instalacao)
                .Include(i => i.InstalacoesPagtoInstal)                               
                     .ThenInclude(c => c.InstalacaoTipoParcela)   
                .Include(i => i.InstalacoesPagtoInstal)                            
                     .ThenInclude(c => c.InstalacaoMotivoMulta)                                                  
                .FirstOrDefault(i => i.CodInstalPagto == codigo);
        }

        public PagedList<InstalacaoPagto> ObterPorParametros(InstalacaoPagtoParameters parameters)
        {
            var query = _context.InstalacaoPagto   
                .Include(i => i.Contrato)
                .Include(i => i.InstalacoesPagtoInstal)
                     .ThenInclude(c => c.Instalacao)     
                .Include(i => i.InstalacoesPagtoInstal)                               
                     .ThenInclude(c => c.InstalacaoTipoParcela)   
                .Include(i => i.InstalacoesPagtoInstal)                            
                     .ThenInclude(c => c.InstalacaoMotivoMulta)   
                .AsNoTracking() 
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(i =>
                    i.CodInstalPagto.ToString().Contains(parameters.Filter) ||
                    i.CodContrato.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)                 
                );
            }

            if (parameters.CodContrato.HasValue)
            {
                query = query.Where(i => i.CodContrato == parameters.CodContrato);
            }

            if (parameters.CodInstalPagto.HasValue)
            {
                query = query.Where(i => i.CodInstalPagto == parameters.CodInstalPagto);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<InstalacaoPagto>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
