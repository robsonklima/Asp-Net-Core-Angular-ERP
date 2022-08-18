using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System;
using System.Linq;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Repository
{
    public class AuditoriaVeiculoRepository : IAuditoriaVeiculoRepository
    {
        private readonly AppDbContext _context;

        public AuditoriaVeiculoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(AuditoriaVeiculo auditoriaVeiculo)
        {   
            _context.ChangeTracker.Clear();
            AuditoriaVeiculo a = _context.AuditoriaVeiculo.SingleOrDefault(a => a.CodAuditoriaVeiculo == auditoriaVeiculo.CodAuditoriaVeiculo);

            if (a != null)
            {
                try
                {
                    _context.Entry(a).CurrentValues.SetValues(auditoriaVeiculo);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public void Criar(AuditoriaVeiculo auditoriaVeiculo)
        {
            try
            {
                _context.Add(auditoriaVeiculo);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            AuditoriaVeiculo a = _context.AuditoriaVeiculo.SingleOrDefault(a => a.CodAuditoriaVeiculo == codigo);

            if (a != null)
            {
                try
                {
                    _context.AuditoriaVeiculo.Remove(a);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public AuditoriaVeiculo ObterPorCodigo(int codigo)
        {
            return _context.AuditoriaVeiculo.SingleOrDefault(a => a.CodAuditoriaVeiculo == codigo);
        }

        public PagedList<AuditoriaVeiculo> ObterPorParametros(AuditoriaVeiculoParameters parameters)
        {
            var auditoriasVeiculos = _context.AuditoriaVeiculo.AsQueryable();

            if (parameters.Placa != null)
                auditoriasVeiculos = auditoriasVeiculos.Where(a => a.Placa == parameters.Placa);

            if (!string.IsNullOrWhiteSpace(parameters.SortActive) && !string.IsNullOrWhiteSpace(parameters.SortDirection))
                auditoriasVeiculos = auditoriasVeiculos.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<AuditoriaVeiculo>.ToPagedList(auditoriasVeiculos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
