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
    public class AuditoriaVeiculoAcessorioRepository : IAuditoriaVeiculoAcessorioRepository
    {
        private readonly AppDbContext _context;

        public AuditoriaVeiculoAcessorioRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(AuditoriaVeiculoAcessorio auditoriaVeiculoAcessorio)
        {   
            _context.ChangeTracker.Clear();
            AuditoriaVeiculoAcessorio a = _context.AuditoriaVeiculoAcessorio.SingleOrDefault(a => a.CodAuditoriaVeiculoAcessorio == auditoriaVeiculoAcessorio.CodAuditoriaVeiculoAcessorio);

            if (a != null)
            {
                try
                {
                    _context.Entry(a).CurrentValues.SetValues(auditoriaVeiculoAcessorio);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public void Criar(AuditoriaVeiculoAcessorio auditoriaVeiculoAcessorio)
        {
            try
            {
                _context.Add(auditoriaVeiculoAcessorio);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            AuditoriaVeiculoAcessorio a = _context.AuditoriaVeiculoAcessorio.SingleOrDefault(a => a.CodAuditoriaVeiculoAcessorio == codigo);

            if (a != null)
            {
                try
                {
                    _context.AuditoriaVeiculoAcessorio.Remove(a);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public AuditoriaVeiculoAcessorio ObterPorCodigo(int codigo)
        {
            return _context.AuditoriaVeiculoAcessorio.SingleOrDefault(a => a.CodAuditoriaVeiculoAcessorio == codigo);
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
