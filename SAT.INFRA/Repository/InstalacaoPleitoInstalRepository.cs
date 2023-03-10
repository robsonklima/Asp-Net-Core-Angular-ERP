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
    public partial class InstalacaoPleitoInstalRepository : IInstalacaoPleitoInstalRepository
    {
        private readonly AppDbContext _context;

        public InstalacaoPleitoInstalRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(InstalacaoPleitoInstal instalacaoPleitoInstal)
        {
            _context.ChangeTracker.Clear();
            InstalacaoPleitoInstal inst = _context.InstalacaoPleitoInstal
                .FirstOrDefault(i => (i.CodInstalPleito == instalacaoPleitoInstal.CodInstalPleito)
                    && (i.CodInstalacao == instalacaoPleitoInstal.CodInstalacao));

            if (inst != null)
            {
                _context.Entry(inst).CurrentValues.SetValues(instalacaoPleitoInstal);
                _context.SaveChanges();
            }
        }

        public void Criar(InstalacaoPleitoInstal instalacaoPleitoInstal)
        {
            _context.Add(instalacaoPleitoInstal);
            _context.SaveChanges();
        }

        public void Deletar(int CodInstalacao, int CodInstalPleito)
        {
            InstalacaoPleitoInstal inst = _context.InstalacaoPleitoInstal
                .FirstOrDefault(i => (i.CodInstalPleito == CodInstalPleito)
                    && (i.CodInstalacao == CodInstalacao));

            if (inst != null)
            {
                _context.InstalacaoPleitoInstal.Remove(inst);
                _context.SaveChanges();
            }
        }
        public InstalacaoPleitoInstal ObterPorCodigo(int codInstalPleito)
        {

            return _context.InstalacaoPleitoInstal
                .Include(i => i.Instalacao.EquipamentoContrato.Contrato.ContratosEquipamento)
                .Include(i => i.Instalacao.Filial)                  
                .Include(i => i.Instalacao.Contrato)
                .Include(i => i.Instalacao.EquipamentoContrato)
                .Include(i => i.Instalacao.LocalAtendimentoSol.Cidade.UnidadeFederativa)
                .Include(i => i.Instalacao.Equipamento)
                .Include(i => i.Instalacao.OrdemServico.RelatoriosAtendimento)
                .FirstOrDefault(i => i.CodInstalPleito == codInstalPleito);
        }

        public PagedList<InstalacaoPleitoInstal> ObterPorParametros(InstalacaoPleitoInstalParameters parameters)
        {
            var instalacoes = _context.InstalacaoPleitoInstal
                .Include(i => i.Instalacao.EquipamentoContrato.Contrato.ContratosEquipamento)
                .Include(i => i.Instalacao.Filial)
                .Include(i => i.Instalacao.Contrato)
                .Include(i => i.Instalacao.EquipamentoContrato)
                .Include(i => i.Instalacao.LocalAtendimentoSol.Cidade.UnidadeFederativa)
                .Include(i => i.Instalacao.Equipamento)
                .Include(i => i.Instalacao.OrdemServico.RelatoriosAtendimento)
                .AsNoTracking() 
                .AsQueryable();

            if (parameters.Filter != null)
            {
                instalacoes = instalacoes.Where(i =>
                    i.CodInstalPleito.ToString().Contains(parameters.Filter) ||
                    i.CodInstalacao.ToString().Contains(parameters.Filter)               
                );
            }
            
            if (parameters.CodInstalPleito.HasValue)
            {
                instalacoes = instalacoes.Where(i => i.CodInstalPleito == parameters.CodInstalPleito);
            }

            if (parameters.CodInstalacao.HasValue)
            {
                instalacoes = instalacoes.Where(i => i.CodInstalacao == parameters.CodInstalacao);
            }

            if (parameters.CodEquipContrato.HasValue)
            {
                instalacoes = instalacoes.Where(i => i.CodEquipContrato == parameters.CodEquipContrato);
            }            

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                instalacoes = instalacoes.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<InstalacaoPleitoInstal>.ToPagedList(instalacoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
