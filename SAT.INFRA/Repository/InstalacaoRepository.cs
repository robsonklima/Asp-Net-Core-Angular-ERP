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
    public partial class InstalacaoRepository : IInstalacaoRepository


    {
        private readonly AppDbContext _context;

        public InstalacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public Instalacao Atualizar(Instalacao instalacao)
        {
            _context.ChangeTracker.Clear();
            Instalacao inst = _context.Instalacao.FirstOrDefault(i => i.CodInstalacao == instalacao.CodInstalacao);

            if (inst != null)
            {
                _context.Entry(inst).CurrentValues.SetValues(instalacao);
                _context.SaveChanges();
            }

            return inst;
        }

        public Instalacao Criar(Instalacao instalacao)
        {
            _context.Add(instalacao);
            _context.SaveChanges();
            return instalacao;
        }

        public void Deletar(int codigo)
        {
            Instalacao inst = _context.Instalacao.FirstOrDefault(i => i.CodInstalacao == codigo);

            if (inst != null)
            {
                _context.Instalacao.Remove(inst);
                _context.SaveChanges();
            }
        }

        public Instalacao ObterPorCodigo(int codigo)
        {

            var data = _context.Instalacao
                .Include(i => i.Cliente)
                .Include(i => i.Filial)
                .Include(i => i.Equipamento)
                .Include(i => i.EquipamentoContrato!)
                    .DefaultIfEmpty()                    
                .Include(c => c.Contrato.ContratosEquipamento)
                .Include(i => i.InstalacaoLote)
                .Include(i => i.LocalAtendimentoEnt)
                .Include(i => i.LocalAtendimentoIns)
                .Include(i => i.LocalAtendimentoSol)
                .Include(i => i.OrdemServico)
                .Include(i => i.OrdemServico.RelatoriosAtendimento)
                .Include(i => i.InstalacaoStatus)
                .Include(i => i.InstalacoesRessalva)
                    .ThenInclude(i => i.InstalacaoMotivoRes)
                .FirstOrDefault(i => i.CodInstalacao == codigo);

            return data;
        }

        public PagedList<Instalacao> ObterPorParametros(InstalacaoParameters parameters)
        {
            var instalacoes = _context.Instalacao
                .Include(i => i.Cliente)
                .Include(i => i.Filial!)
                    .DefaultIfEmpty()
                .Include(i => i.Equipamento!)
                    .DefaultIfEmpty()
                .Include(i => i.EquipamentoContrato.LocalAtendimento.Cidade.UnidadeFederativa!)
                    .DefaultIfEmpty()                    
                .Include(c => c.Contrato.ContratosEquipamento)
                .Include(i => i.InstalacaoLote!)
                    .DefaultIfEmpty()
                .Include(i => i.Contrato)
                .Include(i => i.LocalAtendimentoEnt!)
                    .DefaultIfEmpty()
                .Include(i => i.LocalAtendimentoIns!)
                    .DefaultIfEmpty()
                .Include(i => i.LocalAtendimentoSol.Cidade.UnidadeFederativa)
                .Include(i => i.OrdemServico!.StatusServico)
                .Include(i => i.OrdemServico!)
                    .ThenInclude(r => r.RelatoriosAtendimento)
                    .DefaultIfEmpty()                    
                .Include(i => i.InstalacaoStatus!)
                    .DefaultIfEmpty()
                .Include(i => i.Autorizada!)
                    .DefaultIfEmpty()
                .Include(i => i.Regiao!)
                    .DefaultIfEmpty()
                .Include(i => i.Transportadora!)
                    .DefaultIfEmpty()
                .Include(i => i.InstalacaoNFAut!)
                    .DefaultIfEmpty()
                .Include(i => i.InstalacaoNFVenda!)
                    .DefaultIfEmpty()                
                .AsNoTracking()
                .AsQueryable();

            if (parameters.Filter != null)
            {
                instalacoes = instalacoes.Where(p =>
                    p.CodInstalacao.ToString().Contains(parameters.Filter) ||
                    p.EquipamentoContrato.NumSerie.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodContrato != null)
            {
                instalacoes = instalacoes.Where(i => i.CodContrato == parameters.CodContrato);
            }

            if (parameters.CodInstalacao != null)
            {
                instalacoes = instalacoes.Where(i => i.CodInstalacao == parameters.CodInstalacao);
            }

            if (parameters.CodInstalLote != null)
            {
                instalacoes = instalacoes.Where(i => i.CodInstalLote == parameters.CodInstalLote);
            }

            if (parameters.CodCliente != null)
            {
                instalacoes = instalacoes.Where(i => i.CodCliente == parameters.CodCliente);
            }            

            if (parameters.CodEquipContrato != null)
            {
                instalacoes = instalacoes.Where(i => i.CodEquipContrato == parameters.CodEquipContrato);
            }            

            if (!string.IsNullOrWhiteSpace(parameters.CodInstalacoes))
            {
                int[] cods = parameters.CodInstalacoes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                instalacoes = instalacoes.Where(i => cods.Contains(i.CodInstalacao));
            }            

            if (!string.IsNullOrWhiteSpace(parameters.CodEquips))
            {
                int[] cods = parameters.CodEquips.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                instalacoes = instalacoes.Where(i => cods.Contains(i.Equipamento.CodEquip));
            }   

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                instalacoes = instalacoes.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }
            return PagedList<Instalacao>.ToPagedList(instalacoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
