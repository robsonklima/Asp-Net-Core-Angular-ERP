using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class OrdemServicoRelatorioInstalacaoNaoConformidadeRepository : IOrdemServicoRelatorioInstalacaoNaoConformidadeRepository
    {
        private readonly AppDbContext _context;

        public OrdemServicoRelatorioInstalacaoNaoConformidadeRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(OrdemServicoRelatorioInstalacaoNaoConformidade relatorioInstalacaoNaoConforme)
        {
            var relatorio = _context.OrdemServicoRelatorioInstalacaoNaoConformidade.FirstOrDefault(rat => rat.CodOS == relatorioInstalacaoNaoConforme.CodOS);

            if (relatorio != null)
            {
                _context.Entry(relatorio).CurrentValues.SetValues(relatorioInstalacaoNaoConforme);
                _context.SaveChanges();
            }
        }

        public void Criar(OrdemServicoRelatorioInstalacaoNaoConformidade relatorioInstalacaoNaoConforme)
        {
            _context.Add(relatorioInstalacaoNaoConforme);
            _context.SaveChanges();
        }

        public void Deletar(int codOS)
        {
            var relNaoConformidade = _context.OrdemServicoRelatorioInstalacaoNaoConformidade.FirstOrDefault(rat => rat.CodOS == codOS);

            if (relNaoConformidade != null)
            {
                _context.OrdemServicoRelatorioInstalacaoNaoConformidade.Remove(relNaoConformidade);
                _context.SaveChanges();
            }
        }

        public OrdemServicoRelatorioInstalacaoNaoConformidade ObterPorCodigo(int codigo)
        {
            var relatorio = _context.OrdemServicoRelatorioInstalacaoNaoConformidade
                                .Include(r => r.OrdemServicoRelatorioInstalacaoNaoConformidadeItem)
                                .FirstOrDefault(r => r.CodOSRelatorioInstalacao == codigo);

            return relatorio;
        }

        public PagedList<OrdemServicoRelatorioInstalacaoNaoConformidade> ObterPorParametros(OrdemServicoRelatorioInstalacaoNaoConformidadeParameters parameters)
        {
            var relatorios = _context.OrdemServicoRelatorioInstalacaoNaoConformidade
                                .Include(r => r.OrdemServicoRelatorioInstalacaoNaoConformidadeItem)
                                .AsQueryable();
   

            if (parameters.CodOS != null)
            {
                relatorios = relatorios.Where(r => r.CodOS == parameters.CodOS);
            }   

            var a = relatorios.ToQueryString();        

            return PagedList<OrdemServicoRelatorioInstalacaoNaoConformidade>.ToPagedList(relatorios, parameters.PageNumber, parameters.PageSize);
        }
    }
}
