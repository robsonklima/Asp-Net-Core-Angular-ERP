using SAT.INFRA.Context;
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

        public void Atualizar(Instalacao instalacao)
        {
            _context.ChangeTracker.Clear();
            Instalacao inst = _context.Instalacao.FirstOrDefault(i => i.CodInstalacao == instalacao.CodInstalacao);

            if (inst != null)
            {
                _context.Entry(inst).CurrentValues.SetValues(instalacao);
                _context.SaveChanges();
            }
        }

        public void Criar(Instalacao instalacao)
        {
            _context.Add(instalacao);
            _context.SaveChanges();
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
                .Include(i => i.EquipamentoContrato)
                .Include(i => i.InstalacaoLote)
                .Include(i => i.Contrato)
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
                .Include(i => i.Filial)
                .Include(i => i.Equipamento)
                .Include(i => i.EquipamentoContrato)
                .Include(i => i.InstalacaoLote)
                .Include(i => i.Contrato)
                .Include(i => i.LocalAtendimentoEnt)
                .Include(i => i.LocalAtendimentoIns)
                .Include(i => i.LocalAtendimentoSol)
                .Include(i => i.OrdemServico)
                .Include(i => i.InstalacaoStatus)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                instalacoes = instalacoes.Where(p =>
                    p.CodInstalacao.ToString().Contains(parameters.Filter)
                );
            }

            if (parameters.CodContrato != null)
            {
                instalacoes = instalacoes.Where(i => i.CodContrato == parameters.CodContrato);
            }            

            if (parameters.CodInstalLote != null)
            {
                instalacoes = instalacoes.Where(i => i.CodInstalLote == parameters.CodInstalLote);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                instalacoes = instalacoes.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            //var a = instalacoes.ToQueryString();

            return PagedList<Instalacao>.ToPagedList(instalacoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
