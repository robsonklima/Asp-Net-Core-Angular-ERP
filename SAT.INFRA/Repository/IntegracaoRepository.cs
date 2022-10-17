using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class IntegracaoRepository : IIntegracaoRepository
    {
        private readonly AppDbContext _context;

        public IntegracaoRepository(AppDbContext context)
        {
            _context = context;
        }


        public Integracao Criar(Integracao ordem)
        {
            try
            {
                _context.Add(ordem);
                _context.SaveChanges();

                return ordem;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Integracao> ObterPorParametros(IntegracaoParameters parameters)
        {
            var ordens = _context.Integracao.AsQueryable();

            if(parameters.CodCliente.HasValue)
            {
                ordens = ordens.Where(o => o.CodCliente == parameters.CodCliente);
            }

            return ordens.ToList();
        }
    }
}
