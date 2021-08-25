using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repositories
{
    public class AcaoRepository : IAcaoRepository
    {
        private readonly AppDbContext _context;

        public AcaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Acao acao)
        {
            Acao a = _context.Acao.SingleOrDefault(a => a.CodAcao == acao.CodAcao);

            if (a != null)
            {
                try
                {
                    _context.Entry(a).CurrentValues.SetValues(acao);
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
                }
            }
        }

        public void Criar(Acao acao)
        {
            try
            {
                _context.Add(acao);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            }
        }

        public void Deletar(int codigo)
        {
            Acao a = _context.Acao.SingleOrDefault(a => a.CodAcao == codigo);

            if (a != null)
            {
                try
                {
                    _context.Acao.Remove(a);
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_DELETAR);
                }
            }
        }

        public Acao ObterPorCodigo(int codigo)
        {
            return _context.Acao.SingleOrDefault(a => a.CodAcao == codigo);
        }

        public PagedList<Acao> ObterPorParametros(AcaoParameters parameters)
        {
            var acoes = _context.Acao.AsQueryable();

            if (parameters.Filter != null)
            {
                acoes = acoes.Where(
                    c =>
                    c.NomeAcao.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    c.CodEAcao.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    c.CodAcao.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodAcao != null)
            {
                acoes = acoes.Where(a => a.CodAcao == parameters.CodAcao);
            }

            if (parameters.IndAtivo != null)
            {
                acoes = acoes.Where(a => a.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                acoes = acoes.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Acao>.ToPagedList(acoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
