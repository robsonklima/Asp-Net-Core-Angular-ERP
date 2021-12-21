using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;


namespace SAT.INFRA.Repository
{
    public class AutorizadaRepository : IAutorizadaRepository
    {
        private readonly AppDbContext _context;

        public AutorizadaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Autorizada autorizada)
        {
            Autorizada aut = _context.Autorizada.SingleOrDefault(a => a.CodAutorizada == autorizada.CodAutorizada);

            if (aut != null)
            {
                _context.Entry(aut).CurrentValues.SetValues(autorizada);

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
                }
            }
        }

        public void Criar(Autorizada autorizada)
        {
            try
            {
                _context.Add(autorizada);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            }
        }

        public void Deletar(int codigo)
        {
            Autorizada aut = _context.Autorizada.SingleOrDefault(a => a.CodAutorizada == codigo);

            if (aut != null)
            {
                _context.Autorizada.Remove(aut);

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

        public Autorizada ObterPorCodigo(int codigo)
        {
            return _context.Autorizada.Where(a => a.CodAutorizada == codigo)
                        .Include(f => f.Cidade)
                        .Include(f => f.Cidade.UnidadeFederativa)
                        .Include(f => f.Cidade.UnidadeFederativa.Pais)
                   .SingleOrDefault();
        }

        public PagedList<Autorizada> ObterPorParametros(AutorizadaParameters parameters)
        {
            var autorizadas = _context.Autorizada
                .Include(a => a.Filial)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
                autorizadas = autorizadas.Where(a => a.NomeFantasia.Contains(parameters.Filter) ||
                         a.RazaoSocial.Contains(parameters.Filter) ||
                         a.CNPJ.Contains(parameters.Filter));

            if (parameters.CodAutorizada.HasValue)
                autorizadas = autorizadas.Where(a => a.CodAutorizada == parameters.CodAutorizada);

            if (!string.IsNullOrWhiteSpace(parameters.CodAutorizadas))
                autorizadas = autorizadas
                    .Where(a => parameters.CodAutorizadas.Contains(a.CodAutorizada.ToString()));

            if (!string.IsNullOrEmpty(parameters.CodFiliais))
            {
                int[] cods = parameters.CodFiliais.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                autorizadas = autorizadas.Where(os => cods.Contains(os.CodFilial));
            }

            if (parameters.CodFilial.HasValue)
                autorizadas = autorizadas.Where(a => a.CodFilial == parameters.CodFilial);

            if (parameters.IndAtivo.HasValue)
                autorizadas = autorizadas.Where(a => a.IndAtivo == parameters.IndAtivo);


            if (!string.IsNullOrWhiteSpace(parameters.SortActive) && !string.IsNullOrWhiteSpace(parameters.SortDirection))
                autorizadas = autorizadas.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<Autorizada>.ToPagedList(autorizadas, parameters.PageNumber, parameters.PageSize);
        }
    }
}
