using Microsoft.EntityFrameworkCore;
using SAT.API.Context;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Helpers;
using System;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.API.Repositories
{
    public class LocalAtendimentoRepository : ILocalAtendimentoRepository
    {
        private readonly AppDbContext _context;
        private readonly ILoggerRepository _logger;

        public LocalAtendimentoRepository(AppDbContext context, ILoggerRepository logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Atualizar(LocalAtendimento localAtendimento)
        {
            LocalAtendimento local = _context.LocalAtendimento.SingleOrDefault(l => l.CodPosto == localAtendimento.CodPosto);
            if (local != null)
            {
                _context.Entry(local).CurrentValues.SetValues(localAtendimento);

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex.InnerException.Message);

                    throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
                }
            }
        }

        public void Criar(LocalAtendimento localAtendimento)
        {
            try
            {
                _context.Add(localAtendimento);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            }
        }

        public void Deletar(int codigo)
        {
            var localAtendimento = _context.LocalAtendimento.SingleOrDefault(l => l.CodPosto == codigo);

            if (localAtendimento != null)
            {
                _context.LocalAtendimento.Remove(localAtendimento);

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_DELETAR);
                }
            }
        }

        public LocalAtendimento ObterPorCodigo(int codigo)
        {
            return _context.LocalAtendimento.FirstOrDefault(f => f.CodPosto == codigo);
        }

        public PagedList<LocalAtendimento> ObterPorParametros(LocalAtendimentoParameters parameters)
        {
            var locais = _context.LocalAtendimento
                .Include(l => l.Regiao)
                .Include(l => l.Autorizada)
                .Include(l => l.Filial)
                .Include(l => l.Cliente)
                .Include(l => l.Cidade)
                .Include(l => l.TipoRota)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                locais = locais.Where(
                    l =>
                    l.CodPosto.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    l.NomeLocal.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    l.DCPosto.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    l.NumAgencia.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodPosto != null)
            {
                locais = locais.Where(l => l.CodPosto == parameters.CodPosto);
            }

            if (parameters.CodCliente != null)
            {
                locais = locais.Where(l => l.CodCliente == parameters.CodCliente);
            }

            if (parameters.NumAgencia != null)
            {
                locais = locais.Where(l => l.NumAgencia == parameters.NumAgencia);
            }

            if (parameters.DCPosto != null)
            {
                locais = locais.Where(l => l.DCPosto == parameters.DCPosto);
            }

            if (parameters.IndAtivo != null)
            {
                locais = locais.Where(l => l.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                locais = locais.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<LocalAtendimento>.ToPagedList(locais, parameters.PageNumber, parameters.PageSize);
        }
    }
}
