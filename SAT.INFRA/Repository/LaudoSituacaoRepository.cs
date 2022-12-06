using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class LaudoSituacaoRepository : ILaudoSituacaoRepository
    {
        private readonly AppDbContext _context;

        public LaudoSituacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(LaudoSituacao laudoSituacao)
        {
            _context.ChangeTracker.Clear();
            LaudoSituacao l = _context.LaudoSituacao
                .FirstOrDefault(l => l.CodLaudoSituacao == laudoSituacao.CodLaudoSituacao);
            try
            {
                if (l != null)
                {
                    _context.Entry(l).CurrentValues.SetValues(laudoSituacao);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public LaudoSituacao Criar(LaudoSituacao laudoSituacao)
        {
            _context.Add(laudoSituacao);
            _context.SaveChanges();
            return laudoSituacao;
        }

        public LaudoSituacao ObterPorCodigo(int codigo)
        {
            try
            {
                return _context.LaudoSituacao
                .SingleOrDefault(a => a.CodLaudoSituacao == codigo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar a laudo situacao {ex.Message}");
            }
        }

        public PagedList<LaudoSituacao> ObterPorParametros(LaudoSituacaoParameters parameters)
        {
            var laudos = _context.LaudoSituacao
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                laudos = laudos.Where(a =>
                    a.CodLaudoSituacao.ToString().Contains(parameters.Filter));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                laudos = laudos.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<LaudoSituacao>.ToPagedList(laudos, parameters.PageNumber, parameters.PageSize);
        }

    }
}