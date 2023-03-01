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
    public partial class InstalacaoPleitoRepository : IInstalacaoPleitoRepository
    {
        private readonly AppDbContext _context;

        public InstalacaoPleitoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(InstalacaoPleito instalacaoPleito)
        {
            _context.ChangeTracker.Clear();
            InstalacaoPleito inst = _context.InstalacaoPleito.FirstOrDefault(i => i.CodInstalPleito == instalacaoPleito.CodInstalPleito);

            if (inst != null)
            {
                _context.Entry(inst).CurrentValues.SetValues(instalacaoPleito);
                _context.SaveChanges();
            }
        }

        public void Criar(InstalacaoPleito instalacaoPleito)
        {
            _context.Add(instalacaoPleito);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            InstalacaoPleito inst = _context.InstalacaoPleito.FirstOrDefault(i => i.CodInstalPleito == codigo);

            if (inst != null)
            {
                _context.InstalacaoPleito.Remove(inst);
                _context.SaveChanges();
            }
        }

        public InstalacaoPleito ObterPorCodigo(int codigo)
        {
            return _context.InstalacaoPleito
                .Include(i => i.Contrato)   
                .Include(i => i.InstalacaoTipoPleito)
                .Include(i => i.InstalacoesPleitoInstal)
                    .ThenInclude(c => c.Instalacao.EquipamentoContrato.Contrato)
                .Include(i => i.InstalacoesPleitoInstal)
                    .ThenInclude(c => c.Instalacao.InstalacaoLote)
                .Include(i => i.InstalacoesPleitoInstal)
                    .ThenInclude(c => c.Instalacao.LocalAtendimentoIns)
                .Include(i => i.InstalacoesPleitoInstal)
                    .ThenInclude(c => c.Instalacao.Filial)
                .Include(i => i.InstalacoesPleitoInstal)
                    .ThenInclude(c => c.Instalacao.Equipamento)
                .Include(i => i.InstalacoesPleitoInstal)
                    .ThenInclude(c => c.Instalacao.InstalacaoNFVenda)
                .Include(i => i.InstalacoesPleitoInstal)
                    .ThenInclude(c => c.Instalacao.LocalAtendimentoIns.Cidade.UnidadeFederativa)
                // .Include(i => i.InstalacoesPleitoInstal)
                //     .ThenInclude(c => c.Instalacao)
                //         .ThenInclude(c => c.Contrato)
                //             .ThenInclude(c => c.ContratosEquipamento)
                .FirstOrDefault(i => i.CodInstalPleito == codigo);
        }

        public PagedList<InstalacaoPleito> ObterPorParametros(InstalacaoPleitoParameters parameters)
        {
            var instalacoes = _context.InstalacaoPleito
                .Include(i => i.Contrato)
                .Include(i => i.InstalacaoTipoPleito)
                .Include(i => i.InstalacoesPleitoInstal)
                    .ThenInclude(c => c.Instalacao.EquipamentoContrato.Cliente)
                .AsNoTracking() 
                .AsQueryable();

            if (parameters.Filter != null)
            {
                instalacoes = instalacoes.Where(i =>
                    i.CodInstalPleito.ToString().Contains(parameters.Filter) ||
                    i.CodInstalTipoPleito.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    i.CodContrato.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)                 
                );
            }
            
            if (!string.IsNullOrWhiteSpace(parameters.CodClientes))
            {
                int[] cods = parameters.CodClientes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                instalacoes = instalacoes.Where(i => cods.Contains(i.Contrato.CodCliente));
            } 

            if (parameters.CodContrato.HasValue)
            {
                instalacoes = instalacoes.Where(i => i.CodContrato == parameters.CodContrato);
            }

            if (parameters.CodInstalPleito.HasValue)
            {
                instalacoes = instalacoes.Where(i => i.CodInstalPleito == parameters.CodInstalPleito);
            }

            if (parameters.CodInstalTipoPleito.HasValue)
            {
                instalacoes = instalacoes.Where(i => i.CodInstalTipoPleito == parameters.CodInstalTipoPleito);
            }       

            if (!string.IsNullOrWhiteSpace(parameters.CodInstalTipoPleitos))
            {
                int[] cods = parameters.CodInstalTipoPleitos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                instalacoes = instalacoes.Where(i => cods.Contains(i.InstalacaoTipoPleito.CodInstalTipoPleito));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                instalacoes = instalacoes.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<InstalacaoPleito>.ToPagedList(instalacoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
