using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class MotivoAgendamentoRepository : IMotivoAgendamentoRepository
    {
        private readonly AppDbContext _context;

        public MotivoAgendamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(MotivoAgendamento motivoAgendamento)
        {
            _context.ChangeTracker.Clear();
            MotivoAgendamento a = _context.MotivoAgendamento.SingleOrDefault(a => a.CodMotivo == motivoAgendamento.CodMotivo);

            if (a != null)
            {
                try
                {
                    _context.Entry(a).CurrentValues.SetValues(motivoAgendamento);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public void Criar(MotivoAgendamento motivoAgendamento)
        {
            try
            {
                _context.Add(motivoAgendamento);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            MotivoAgendamento a = _context.MotivoAgendamento.SingleOrDefault(a => a.CodMotivo == codigo);

            if (a != null)
            {
                try
                {
                    _context.MotivoAgendamento.Remove(a);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public MotivoAgendamento ObterPorCodigo(int codigo)
        {
            throw new System.NotImplementedException();
        }

        public PagedList<MotivoAgendamento> ObterPorParametros(MotivoAgendamentoParameters parameters)
        {
            var motivos = _context.MotivoAgendamento.AsQueryable();

            if (parameters.Filter != null)
            {
                motivos = motivos.Where(
                    c =>
                    c.DescricaoMotivo.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    c.CodMotivo.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodMotivo != null)
            {
                motivos = motivos.Where(a => a.CodMotivo == parameters.CodMotivo);
            }

            if (parameters.IndAtivo != null)
            {
                motivos = motivos.Where(a => a.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                motivos = motivos.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<MotivoAgendamento>.ToPagedList(motivos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
