using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Reflection;

namespace SAT.INFRA.Repository
{
    public class OrdemServicoRelatorioInstalacaoRepository : IOrdemServicoRelatorioInstalacaoRepository
    {
        private readonly AppDbContext _context;

        public OrdemServicoRelatorioInstalacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(OrdemServicoRelatorioInstalacao relatorioInstalacao)
        {
            var relatorio = _context.OrdemServicoRelatorioInstalacao.FirstOrDefault(rat => rat.CodOS == relatorioInstalacao.CodOS);

            if (relatorio != null)
            {
                _context.Entry(relatorio).CurrentValues.SetValues(relatorioInstalacao);
                _context.SaveChanges();
            }
        }

        public void Criar(OrdemServicoRelatorioInstalacao relatorioInstalacao)
        {
            _context.Add(relatorioInstalacao);
            _context.SaveChanges();
        }

        public void Deletar(int codOS)
        {
            var relInst = _context.OrdemServicoRelatorioInstalacao.FirstOrDefault(rat => rat.CodOS == codOS);

            if (relInst != null)
            {
                _context.OrdemServicoRelatorioInstalacao.Remove(relInst);
                _context.SaveChanges();
            }
        }

        public OrdemServicoRelatorioInstalacao ObterPorCodigo(int codigo)
        {
            var relatorio = _context.OrdemServicoRelatorioInstalacao
                .Include(r => r.OrdemServicoRelatorioInstalacaoItem)
                // .Include(r => r.OrdemServicoRelatorioInstalacaoNaoConformidade)
                .FirstOrDefault(r => r.CodOSRelatorioInstalacao == codigo);

            return relatorio;
        }

        public PagedList<OrdemServicoRelatorioInstalacao> ObterPorParametros(OrdemServicoRelatorioInstalacaoParameters parameters)
        {
            var relatorios = _context.OrdemServicoRelatorioInstalacao
                .Include(r => r.OrdemServicoRelatorioInstalacaoItem)
                // .Include(r => r.OrdemServicoRelatorioInstalacaoNaoConformidade)
                .AsQueryable();

                
   

            if (parameters.CodOS != null)
            {
                relatorios = relatorios.Where(r => r.CodOS == parameters.CodOS);
            }   

            var a = relatorios.ToQueryString();        

            return PagedList<OrdemServicoRelatorioInstalacao>.ToPagedList(relatorios, parameters.PageNumber, parameters.PageSize);
        }
    }
}
