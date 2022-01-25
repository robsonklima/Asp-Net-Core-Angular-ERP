using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class ContratoSLARepository : IContratoSLARepository
    {
        private readonly AppDbContext _context;

        public ContratoSLARepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ContratoSLA contratoSla)
        {
            ContratoSLA cs = _context.ContratoSLA.FirstOrDefault(d => d.CodContrato == contratoSla.CodContrato);

            try
            {
                if (cs != null)
                {
                    _context.Entry(cs).CurrentValues.SetValues(contratoSla);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
            }
        }

        public void Criar(ContratoSLA contratoSla)
        {
            try
            {
                _context.Add(contratoSla);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public void Deletar(int codContrato, int codSLA)
        {
            ContratoSLA cs = _context.ContratoSLA.FirstOrDefault(d => d.CodContrato.Equals(codContrato) && d.CodSLA.Equals(codSLA));

            if (cs != null)
            {
                _context.ContratoSLA.Remove(cs);
                _context.SaveChanges();
            }
        }

        public ContratoSLA ObterPorCodigo(int codContrato, int codSLA)
        {
            return _context.ContratoSLA
                           .Include(c => c.Contrato)
                           .Include(c => c.SLA)
                           .FirstOrDefault(c => c.CodContrato.Equals(codContrato) && c.CodSLA.Equals(codSLA));
        }

        public PagedList<ContratoSLA> ObterPorParametros(ContratoSLAParameters parameters)
        {
            var contratosSLA = _context.ContratoSLA
                .Include(c => c.Contrato)
                .Include(c => c.SLA)
                .AsQueryable();
                
            if (parameters.CodContrato != null)
            {
                contratosSLA = contratosSLA.Where(a => a.CodContrato == parameters.CodContrato);
            }

            if (parameters.CodSLA != null)
            {
                contratosSLA = contratosSLA.Where(a => a.CodSLA == parameters.CodSLA);
            }

            return PagedList<ContratoSLA>.ToPagedList(contratosSLA, parameters.PageNumber, parameters.PageSize);
        }
    }
}
