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
    public class StatusServicoSTNRepository : IStatusServicoSTNRepository
    {
        private readonly AppDbContext _context;

        public StatusServicoSTNRepository(AppDbContext context)
        {
            _context = context;
        }

        public StatusServicoSTN Criar(StatusServicoSTN statusServico)
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
            var statusServico = _context.StatusServicoSTN.SingleOrDefault(s => s.CodStatusServicoSTN == codigo);

            if (statusServico != null)
            {
                _context.StatusServicoSTN.Remove(statusServico);

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

        public void Atualizar(StatusServicoSTN statusServico)
        {
            StatusServicoSTN s = _context.StatusServicoSTN.SingleOrDefault(s => s.CodStatusServicoSTN == statusServico.CodStatusServicoSTN);
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

        public PagedList<StatusServicoSTN> ObterPorParametros(StatusServicoSTNParameters parameters)
        {
            var status = _context.StatusServicoSTN.AsQueryable();

            if (parameters.Filter != null)
            {
                status = status.Where(
                    c =>
                    c.DescStatusServicoSTN.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    c.CodStatusServicoSTN.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            return PagedList<StatusServicoSTN>.ToPagedList(status, parameters.PageNumber, parameters.PageSize);
        }

        public StatusServicoSTN ObterPorCodigo(int codigo)
        {
            return _context.StatusServicoSTN.FirstOrDefault(t => t.CodStatusServicoSTN == codigo);
        }
    }
}
