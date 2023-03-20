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
    public partial class InstalacaoPagtoInstalRepository : IInstalacaoPagtoInstalRepository
    {
        private readonly AppDbContext _context;

        public InstalacaoPagtoInstalRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(InstalacaoPagtoInstal instalacaoPagtoInstal)
        {
            _context.ChangeTracker.Clear();
            InstalacaoPagtoInstal inst = _context.InstalacaoPagtoInstal
                .FirstOrDefault(i => (i.CodInstalPagto == instalacaoPagtoInstal.CodInstalPagto)
                    && (i.CodInstalacao == instalacaoPagtoInstal.CodInstalacao));

            if (inst != null)
            {
                _context.Entry(inst).CurrentValues.SetValues(instalacaoPagtoInstal);
                _context.SaveChanges();
            }
        }

        public void Criar(InstalacaoPagtoInstal instalacaoPagtoInstal)
        {
            _context.Add(instalacaoPagtoInstal);
            _context.SaveChanges();
        }

        public void Deletar(int CodInstalacao, int CodInstalPagto, int CodInstalTipoParcela)
        {
            InstalacaoPagtoInstal inst = _context.InstalacaoPagtoInstal
                .FirstOrDefault(i => (i.CodInstalPagto == CodInstalPagto)
                    && (i.CodInstalacao == CodInstalacao)
                    && (i.CodInstalTipoParcela == CodInstalTipoParcela));

            if (inst != null)
            {
                _context.InstalacaoPagtoInstal.Remove(inst);
                _context.SaveChanges();
            }
        }
        public InstalacaoPagtoInstal ObterPorCodigo(int codInstalacao, int codInstalPagto, int codInstalTipoParcela)
        {

            return _context.InstalacaoPagtoInstal
                .Include(i => i.Instalacao.EquipamentoContrato)                  
                .Include(i => i.Instalacao.Equipamento)
                .Include(i => i.Instalacao.InstalacaoNFVenda)                                
                .Include(i => i.InstalacaoTipoParcela)
                .Include(i => i.InstalacaoMotivoMulta)
                .FirstOrDefault(i => i.CodInstalacao == codInstalacao && i.CodInstalPagto == codInstalPagto && i.CodInstalTipoParcela == codInstalTipoParcela);
        }

        public PagedList<InstalacaoPagtoInstal> ObterPorParametros(InstalacaoPagtoInstalParameters parameters)
        {
            var query = _context.InstalacaoPagtoInstal
                .Include(i => i.Instalacao.EquipamentoContrato)  
                .Include(i => i.Instalacao.Equipamento)  
                .Include(i => i.Instalacao.InstalacaoNFVenda)                                
                .Include(i => i.InstalacaoTipoParcela)
                .Include(i => i.InstalacaoMotivoMulta)
                .AsNoTracking() 
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(i =>
                    i.CodInstalPagto.ToString().Contains(parameters.Filter) ||
                    i.CodInstalacao.ToString().Contains(parameters.Filter)               
                );
            }
            
            if (parameters.CodInstalPagto.HasValue)
            {
                query = query.Where(i => i.CodInstalPagto == parameters.CodInstalPagto);
            }

            if (parameters.CodInstalacao.HasValue)
            {
                query = query.Where(i => i.Instalacao.CodInstalacao == parameters.CodInstalacao);
            }       

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<InstalacaoPagtoInstal>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
