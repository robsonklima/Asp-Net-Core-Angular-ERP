using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class RegiaoAutorizadaRepository : IRegiaoAutorizadaRepository
    {
        private readonly AppDbContext _context;

        public RegiaoAutorizadaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Criar(RegiaoAutorizada regiaoAutorizada)
        {
            try
            {
                _context.Add(regiaoAutorizada);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
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
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
                }
            }
        }

        public RegiaoAutorizada ObterPorCodigo(int codRegiao, int codAutorizada, int codFilial)
        {
            return _context.RegiaoAutorizada
                .Include(ra => ra.Regiao)
                .Include(ra => ra.Autorizada)
                .Include(ra => ra.Filial)
                .Include(ra => ra.Cidade)
                .SingleOrDefault(
                    ra =>
                    ra.CodAutorizada == codAutorizada &&
                    ra.CodRegiao == codRegiao &&
                    ra.CodFilial == codFilial);
        }

        public IQueryable<RegiaoAutorizada> ObterQuery(RegiaoAutorizadaParameters parameters)
        {
            IQueryable<RegiaoAutorizada> query = _context.RegiaoAutorizada.AsQueryable();

            query = AplicarIncludes(query);
            query = AplicarFiltros(query, parameters);
            query = AplicarOrdenacao(query, parameters.SortActive, parameters.SortDirection);

            return query.AsNoTracking();
        }

        public PagedList<RegiaoAutorizada> ObterPorParametros(RegiaoAutorizadaParameters parameters)
        {
           // IQueryable<RegiaoAutorizada> query = this.ObterQuery(parameters);

            var regioesAutorizadas = _context.RegiaoAutorizada
                .Include(ra => ra.Regiao)
                .Include(ra => ra.Autorizada)
                .Include(ra => ra.Filial)
                .Include(ra => ra.Cidade)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                regioesAutorizadas = regioesAutorizadas.Where(
                    ra =>
                    ra.CodRegiao.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    ra.Regiao.NomeRegiao.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)

                );
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodFiliais))
            {
                int[] cods = parameters.CodFiliais.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                regioesAutorizadas = regioesAutorizadas.Where(ra => cods.Contains(ra.Filial.CodFilial));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodAutorizadas))
            {
                int[] cods = parameters.CodAutorizadas.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                regioesAutorizadas = regioesAutorizadas.Where(ra => cods.Contains(ra.CodAutorizada.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodCidades))
            {
                int[] cods = parameters.CodCidades.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                regioesAutorizadas = regioesAutorizadas.Where(ra => cods.Contains(ra.CodCidade.Value));
            }

            if (parameters.IndAtivo != null)
            {
                regioesAutorizadas = regioesAutorizadas.Where(r => r.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                regioesAutorizadas = regioesAutorizadas.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }





            return PagedList<RegiaoAutorizada>.ToPagedList(regioesAutorizadas, parameters.PageNumber, parameters.PageSize);
        }
    }
}
