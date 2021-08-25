using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Helpers;
using System;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repositories
{
    public class LocalAtendimentoRepository : ILocalAtendimentoRepository
    {
        private readonly AppDbContext _context;

        public LocalAtendimentoRepository(AppDbContext context)
        {
            _context = context;
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
                    throw new Exception(ex.Message);
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
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
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
            return _context.LocalAtendimento
                .Include(l => l.Cidade)
                .Include(l => l.Cidade.UnidadeFederativa)
                .Include(l => l.Cidade.UnidadeFederativa.Pais)
                .Include(l => l.Cliente)
                .Include(l => l.TipoRota)
                .Include(l => l.Filial)
                .Include(l => l.Autorizada)
                .Include(l => l.Regiao)
                .FirstOrDefault(f => f.CodPosto == codigo);
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

            if (parameters.CodAutorizada != null)
            {
                locais = locais.Where(l => l.CodAutorizada == parameters.CodAutorizada);
            }

            if (parameters.CodRegiao != null)
            {
                locais = locais.Where(l => l.CodRegiao == parameters.CodRegiao);
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
