using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities.Constants;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class RegiaoRepository : IRegiaoRepository
    {
        private readonly AppDbContext _context;

        public RegiaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Regiao regiao)
        {
            _context.ChangeTracker.Clear();
            Regiao reg = _context.Regiao.SingleOrDefault(r => r.CodRegiao == regiao.CodRegiao);

            if (reg != null)
            {
                try
                {
                    _context.Entry(reg).CurrentValues.SetValues(regiao);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public void Criar(Regiao regiao)
        {
            try
            {
                _context.Add(regiao);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            Regiao reg = _context.Regiao.SingleOrDefault(r => r.CodRegiao == codigo);

            if (reg != null)
            {
                _context.Regiao.Remove(reg);

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

        public Regiao ObterPorCodigo(int codigo)
        {
            return _context.Regiao.SingleOrDefault(r => r.CodRegiao == codigo);
        }

        public PagedList<Regiao> ObterPorParametros(RegiaoParameters parameters)
        {
            var regioes = _context.Regiao
                .AsQueryable();

            if (parameters.Filter != null)
            {
                regioes = regioes.Where(
                    r =>
                    r.CodRegiao.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    r.NomeRegiao.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)

                );
            }

            if (parameters.CodRegiao != null)
            {
                regioes = regioes.Where(r => r.CodRegiao == parameters.CodRegiao);
            }                  

            if (!string.IsNullOrWhiteSpace(parameters.NomeRegiao))
            {
               regioes = regioes.Where(r => r.NomeRegiao == parameters.NomeRegiao);
            }                       

            if (parameters.IndAtivo != null)
            {
                regioes = regioes.Where(r => r.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                regioes = regioes.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<Regiao>.ToPagedList(regioes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
