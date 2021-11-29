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
    public class SLARepository : ISLARepository
    {
        private readonly AppDbContext _context;

        public SLARepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(SLA sla)
        {
            SLA s = _context.SLA.FirstOrDefault(d => d.CodSLA == sla.CodSLA);

            try
            {
                if (s != null)
                {
                    _context.Entry(s).CurrentValues.SetValues(sla);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
            }
        }

        public void Criar(SLA sla)
        {
            _context.Add(sla);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            SLA s = _context.SLA.FirstOrDefault(d => d.CodSLA == codigo);

            if (s != null)
            {
                _context.SLA.Remove(s);
                _context.SaveChanges();
            }
        }

        public SLA ObterPorCodigo(int codigo)
        {
            return _context.SLA
                            .FirstOrDefault(c => c.CodSLA == codigo);
        }

        public PagedList<SLA> ObterPorParametros(SLAParameters parameters)
        {
            var sla = _context.SLA
                                    .AsQueryable();

            if (parameters.NomeSLA != null)
            {
                sla = sla.Where(c => c.NomeSLA.Contains(parameters.NomeSLA));
            }

            if (parameters.TempoInicio != null)
            {
                sla = sla.Where(c => c.TempoInicio == parameters.TempoInicio);
            }

            if (parameters.TempoReparo != null)
            {
                sla = sla.Where(c => c.TempoReparo == parameters.TempoReparo);
            }

            if (parameters.TempoSolucao != null)
            {
                sla = sla.Where(c => c.TempoSolucao == parameters.TempoSolucao);
            }

            return PagedList<SLA>.ToPagedList(sla, parameters.PageNumber, parameters.PageSize);
        }
    }
}
