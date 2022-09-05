using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class AuditoriaRepository : IAuditoriaRepository
    {
        private readonly AppDbContext _context;

        public AuditoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Auditoria auditoria)
        {
            _context.ChangeTracker.Clear();
            Auditoria aud = _context.Auditoria
                .FirstOrDefault(aud => aud.CodAuditoria == auditoria.CodAuditoria);
            try
            {
                if (aud != null)
                {
                    _context.Entry(aud).CurrentValues.SetValues(auditoria);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    

        public void Criar(Auditoria auditoria)
        {
            try
            {
                _context.Add(auditoria);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Deletar(int codAuditoria)
        {
            Auditoria aud = _context.Auditoria
                .FirstOrDefault(aud => aud.CodAuditoria == codAuditoria);

            if (aud != null)
            {
                _context.Auditoria.Remove(aud);
                _context.SaveChanges();
            }
        }

        public Auditoria ObterPorCodigo(int codAuditoria)
        {
            try
            {
                return _context.Auditoria
                    .Include(c => c.Usuario)
                        .ThenInclude(c => c.Filial)
                    .Include(c => c.Usuario)
                        .ThenInclude(c => c.Tecnico)
                            .ThenInclude(c => c.Veiculos)
                    .Include(c => c.AuditoriaStatus)
                    .Include(c => c.AuditoriaVeiculo)
                        .ThenInclude(c => c.Acessorios)
                    .Include(c => c.AuditoriaVeiculo)
                        .ThenInclude(c => c.AuditoriaVeiculoTanque)
                    .Include(c => c.Fotos)
                    .SingleOrDefault(aud => aud.CodAuditoria == codAuditoria);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar a auditoria {ex.Message}");
            }
        }

        public PagedList<Auditoria> ObterPorParametros(AuditoriaParameters parameters)
        {
            var auditorias = _context.Auditoria
                .Include(c => c.Usuario)
                    .ThenInclude(c => c.Filial)
                .Include(c => c.Usuario)
                    .ThenInclude(c => c.Tecnico)
                .Include(c => c.AuditoriaStatus)
                .Include(c => c.AuditoriaVeiculo)
                .AsQueryable();

            if (parameters.CodAuditoriaStatus != null)
            {
                auditorias = auditorias.Where(a => a.CodAuditoriaStatus == parameters.CodAuditoriaStatus);
            };

            if (parameters.CodAuditoriaVeiculo != null)
            {
                auditorias = auditorias.Where(a => a.CodAuditoriaVeiculo == parameters.CodAuditoriaVeiculo);
            };

            if (!string.IsNullOrWhiteSpace(parameters.CodFiliais))
            {
                int[] cods = parameters.CodFiliais.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                auditorias = auditorias.Where(dc => cods.Contains(dc.Usuario.Filial.CodFilial));
            };

            if (!string.IsNullOrWhiteSpace(parameters.CodUsuarios))
            {
               string[] cods = parameters.CodUsuarios.Split(",").Select(a =>a.Trim()).Distinct().ToArray();
                auditorias = auditorias.Where(dc => cods.Contains(dc.Usuario.CodUsuario));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodAuditoriaStats))
            {
                int[] cods = parameters.CodAuditoriaStats.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                auditorias = auditorias.Where(dc => cods.Contains(dc.AuditoriaStatus.CodAuditoriaStatus));
            };

            if (!string.IsNullOrEmpty(parameters.CodAuditoriaStatusNotIn))
            {
                int[] cods = parameters.CodAuditoriaStatusNotIn.Split(',').Select(f => int.Parse(f.Trim())).Distinct().ToArray();
                auditorias = auditorias.Where(e => !cods.Contains(e.CodAuditoriaStatus));
            }

            return PagedList<Auditoria>.ToPagedList(auditorias, parameters.PageNumber, parameters.PageSize);
        }
    }
}
