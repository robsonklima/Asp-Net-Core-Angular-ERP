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
            return _context.Instalacao.FirstOrDefault(f => f.CodInstalacao == codigo);
        }

        public PagedList<Instalacao> ObterPorParametros(InstalacaoParameters parameters)
        {
            var instalacoes = _context.Instalacao
                .Include(i => i.Cliente)
                .Include(i => i.Filial)
                .Include(i => i.Equipamento)
                .Include(i => i.EquipamentoContrato)
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

            return PagedList<Instalacao>.ToPagedList(instalacoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
