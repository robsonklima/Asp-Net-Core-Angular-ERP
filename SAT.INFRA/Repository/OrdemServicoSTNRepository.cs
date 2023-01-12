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

            if (o != null)
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
                    .ThenInclude(o => o.Tecnico)
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
                    .ThenInclude(o => o.Tecnico)
                .Include(o => o.OrdemServico)
                    .ThenInclude(o => o.EquipamentoContrato)
                        .ThenInclude(o => o.Equipamento)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(p =>
                    p.CodAtendimento.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.CodOS.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodOS.HasValue)
            {
                query = query.Where(p => p.CodOS == parameters.CodOS);
            }

            if (parameters.CodAtendimento.HasValue)
            {
                query = query.Where(p => p.CodAtendimento == parameters.CodAtendimento);
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodClientes))
            {
                int[] cods = parameters.CodClientes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.OrdemServico.CodCliente));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodFiliais))
            {
                int[] cods = parameters.CodFiliais.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.OrdemServico.Filial.CodFilial));
            };

            if (!string.IsNullOrEmpty(parameters.CodEquips))
            {
                var modelos = parameters.CodEquips.Split(',').Select(e => e.Trim());
                query = query.Where(os => modelos.Any(p => p == os.OrdemServico.Equipamento.CodEquip.ToString()));
            }

            if (parameters.CodOrigemChamadoSTN != null)
            {
                query = query.Where(a => a.CodOrigemChamadoSTN == parameters.CodOrigemChamadoSTN);
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodOrigemChamadoSTNs))
            {
                int[] cods = parameters.CodOrigemChamadoSTNs.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.OrdemServicoSTNOrigem.CodOrigemChamadoSTN));
            };

            if (!string.IsNullOrEmpty(parameters.CodUsuarios))
            {
                var usuarios = parameters.CodUsuarios.Split(',').Select(e => e.Trim());
                query = query.Where(os => usuarios.Any(p => p == os.Usuario.CodUsuario));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<OrdemServicoSTN>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
