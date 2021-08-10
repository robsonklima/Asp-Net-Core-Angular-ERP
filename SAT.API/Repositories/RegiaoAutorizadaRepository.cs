using Microsoft.EntityFrameworkCore;
using SAT.API.Context;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.API.Repositories
{
    public class RegiaoAutorizadaRepository : IRegiaoAutorizadaRepository
    {
        private readonly AppDbContext _context;

        public RegiaoAutorizadaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(RegiaoAutorizada regiaoAutorizada)
        {
            RegiaoAutorizada ra = _context.RegiaoAutorizada
                                .SingleOrDefault(ra =>
                                                 ra.CodAutorizada == regiaoAutorizada.CodAutorizada &&
                                                 ra.CodRegiao == regiaoAutorizada.CodRegiao &&
                                                 ra.CodFilial == regiaoAutorizada.CodFilial);

            if (ra != null)
            {
                try
                {
                    _context.Entry(ra).CurrentValues.SetValues(regiaoAutorizada);
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
                }
            }
        }

        public void Criar(RegiaoAutorizada regiaoAutorizada)
        {
            try
            {
                _context.Add(regiaoAutorizada);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            }
        }

        public void Deletar(int codRegiao, int codAutorizada, int codFilial)
        {
            RegiaoAutorizada ra = _context.RegiaoAutorizada.SingleOrDefault(
                             ra => ra.CodAutorizada == codAutorizada &&
                                   ra.CodRegiao == codRegiao &&
                                   ra.CodFilial == codFilial);

            if (ra != null)
            {
                _context.RegiaoAutorizada.Remove(ra);

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

        public RegiaoAutorizada ObterPorCodigo(int codRegiao, int codAutorizada, int codFilial)
        {
            return _context.RegiaoAutorizada.SingleOrDefault(
                ra =>
                ra.CodAutorizada == codAutorizada &&
                ra.CodRegiao == codRegiao &&
                ra.CodFilial == codFilial);
        }

        public PagedList<RegiaoAutorizada> ObterPorParametros(RegiaoAutorizadaParameters parameters)
        {
            var regioesAutorizadas = _context.RegiaoAutorizada
                .Include(ra => ra.Cidade)
                .Include(ra => ra.Filial)
                .Include(ra => ra.Autorizada)
                .Include(ra => ra.Regiao)
                .AsQueryable();

            if (parameters.CodRegiao != null)
            {
                regioesAutorizadas = regioesAutorizadas.Where(r => r.CodRegiao == parameters.CodRegiao);
            }

            if (parameters.CodAutorizada != null)
            {
                regioesAutorizadas = regioesAutorizadas.Where(r => r.CodAutorizada == parameters.CodAutorizada);
            }

            if (parameters.CodFilial != null)
            {
                regioesAutorizadas = regioesAutorizadas.Where(r => r.CodFilial == parameters.CodFilial);
            }

            if (parameters.IndAtivo != null)
            {
                regioesAutorizadas = regioesAutorizadas.Where(r => r.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.Filter != null)
            {
                regioesAutorizadas = regioesAutorizadas.Where(
                    ra => ra.CodAutorizada.ToString().Equals(parameters.Filter) ||
                          ra.Autorizada.NomeFantasia.Contains(parameters.Filter) ||
                          ra.Regiao.NomeRegiao.Contains(parameters.Filter) ||
                          ra.Filial.NomeFilial.Contains(parameters.Filter)
                );
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                regioesAutorizadas = regioesAutorizadas.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<RegiaoAutorizada>.ToPagedList(regioesAutorizadas, parameters.PageNumber, parameters.PageSize);
        }
    }
}
