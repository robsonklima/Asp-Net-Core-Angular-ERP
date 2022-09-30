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
    public class OrdemServicoSTNRepository : IOrdemServicoSTNRepository
    {
        private readonly AppDbContext _context;

        public OrdemServicoSTNRepository(AppDbContext context)
        {
            _context = context;
        }

        public OrdemServicoSTN Atualizar(OrdemServicoSTN ordem)
        {
            _context.ChangeTracker.Clear();
            OrdemServicoSTN o = _context.OrdemServicoSTN.FirstOrDefault(p => p.CodAtendimento == ordem.CodAtendimento);

            if(o != null)
            {
                _context.Entry(o).CurrentValues.SetValues(ordem);
                _context.SaveChanges();
            }

            return ordem;
        }

        public OrdemServicoSTN Criar(OrdemServicoSTN ordem)
        {
            _context.Add(ordem);
            _context.SaveChanges();
            return ordem;
        }

        public void Deletar(int codAtendimento)
        {
            OrdemServicoSTN ordem = _context.OrdemServicoSTN.FirstOrDefault(p => p.CodAtendimento == codAtendimento);

            if (ordem != null)
            {
                _context.OrdemServicoSTN.Remove(ordem);
                _context.SaveChanges();
            }
        }

        public OrdemServicoSTN ObterPorCodigo(int codAtendimento)
        {
            return _context.OrdemServicoSTN
                .Include(o => o.StatusSTN)
                .Include(o => o.Usuario)
                .Include(o => o.OrdemServicoSTNOrigem)
                .Include(o => o.OrdemServico)
                    .ThenInclude(o => o.Filial)
                .Include(o => o.OrdemServico)
                    .ThenInclude(o => o.Cliente)
                .Include(o => o.OrdemServico)
                    .ThenInclude(o => o.EquipamentoContrato)
                        .ThenInclude(o => o.Equipamento)
                .FirstOrDefault(p => p.CodAtendimento == codAtendimento);
        }

        public PagedList<OrdemServicoSTN> ObterPorParametros(OrdemServicoSTNParameters parameters)
        {
            var query = _context.OrdemServicoSTN
                .Include(o => o.StatusSTN)
                .Include(o => o.Usuario)
                .Include(o => o.OrdemServicoSTNOrigem)
                .Include(o => o.OrdemServico)
                    .ThenInclude(o => o.Filial)
                .Include(o => o.OrdemServico)
                    .ThenInclude(o => o.Cliente)
                .Include(o => o.OrdemServico)
                    .ThenInclude(o => o.EquipamentoContrato)
                        .ThenInclude(o => o.Equipamento)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(p =>
                    p.CodAtendimento.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<OrdemServicoSTN>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
