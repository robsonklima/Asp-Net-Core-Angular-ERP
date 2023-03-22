using Microsoft.EntityFrameworkCore;
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
    public class StatusServicoRepository : IStatusServicoRepository
    {
        private readonly AppDbContext _context;

        public StatusServicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public StatusServico Criar(StatusServico statusServico)
        {
            try
            {
                _context.Add(statusServico);
                _context.SaveChanges();
                return statusServico;
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            var statusServico = _context.StatusServico.SingleOrDefault(s => s.CodStatusServico == codigo);

            if (statusServico != null)
            {
                _context.StatusServico.Remove(statusServico);

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

        public void Atualizar(StatusServico statusServico)
        {
            StatusServico s = _context.StatusServico.SingleOrDefault(s => s.CodStatusServico == statusServico.CodStatusServico);
            if (s != null)
            {
                _context.Entry(s).CurrentValues.SetValues(statusServico);

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

        public PagedList<StatusServico> ObterPorParametros(StatusServicoParameters parameters)
        {
            var status = _context.StatusServico
                .AsQueryable();

            if (parameters.Filter != null)
            {
                status = status.Where(
                    c =>
                    c.NomeStatusServico.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    c.CodStatusServico.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodStatusServico != null)
            {
                status = status.Where(s => s.CodStatusServico == parameters.CodStatusServico);
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodStatusServicos))
            {
                int[] cods = parameters.CodStatusServicos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                status = status.Where(s => cods.Contains(s.CodStatusServico));
            }

            if (parameters.IndAtivo != null)
            {
                status = status.Where(s => s.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                status = status.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            var a = status.ToQueryString();

            return PagedList<StatusServico>.ToPagedList(status, parameters.PageNumber, parameters.PageSize);
        }

        public StatusServico ObterPorCodigo(int codigo)
        {
            return _context.StatusServico.SingleOrDefault(s => s.CodStatusServico == codigo);
        }
    }
}
