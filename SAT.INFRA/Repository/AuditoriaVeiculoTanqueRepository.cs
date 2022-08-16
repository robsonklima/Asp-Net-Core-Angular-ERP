using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System;
using System.Linq;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Repository
{
    public class AuditoriaVeiculoTanqueRepository : IAuditoriaVeiculoTanqueRepository
    {
        private readonly AppDbContext _context;

        public AuditoriaVeiculoTanqueRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(AuditoriaVeiculoTanque auditoriaVeiculoTanque)
        {   
            _context.ChangeTracker.Clear();
            AuditoriaVeiculoTanque a = _context.AuditoriaVeiculoTanque.SingleOrDefault(a => a.CodAuditoriaVeiculoTaque == auditoriaVeiculoTanque.CodAuditoriaVeiculoTaque);

            if (a != null)
            {
                try
                {
                    _context.Entry(a).CurrentValues.SetValues(auditoriaVeiculoTanque);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public void Criar(AuditoriaVeiculoTanque auditoriaVeiculoTanque)
        {
            try
            {
                _context.Add(auditoriaVeiculoTanque);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            AuditoriaVeiculoTanque a = _context.AuditoriaVeiculoTanque.SingleOrDefault(a => a.CodAuditoriaVeiculoTaque == codigo);

            if (a != null)
            {
                try
                {
                    _context.AuditoriaVeiculoTanque.Remove(a);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public AuditoriaVeiculoTanque ObterPorCodigo(int codigo)
        {
            return _context.AuditoriaVeiculoTanque.SingleOrDefault(a => a.CodAuditoriaVeiculoTaque == codigo);
        }

        // public PagedList<AuditoriaVeiculo> ObterPorParametros(AuditoriaVeiculoParameters parameters)
        // {
        //     var auditoriaVeiculo = _context.AuditoriaVeiculo.AsQueryable();

        //     if (parameters.CodAuditoriaVeiculo.HasValue)
        //         agendamentos = agendamentos.Where(a => a.CodAgendamento == parameters.CodAgendamento);

        //     if (!string.IsNullOrWhiteSpace(parameters.SortActive) && !string.IsNullOrWhiteSpace(parameters.SortDirection))
        //         agendamentos = agendamentos.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

        //     return PagedList<Agendamento>.ToPagedList(agendamentos, parameters.PageNumber, parameters.PageSize);
        // }
    }
}
