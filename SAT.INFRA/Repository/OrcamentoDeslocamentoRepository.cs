using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class OrcamentoDeslocamentoRepository : IOrcamentoDeslocamentoRepository
    {
        private readonly AppDbContext _context;

        public OrcamentoDeslocamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Criar(OrcamentoDeslocamento deslocamento)
        {
            try
            {
                _context.Add(deslocamento);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
            }
        }

        public void Atualizar(OrcamentoDeslocamento deslocamento)
        {
            OrcamentoDeslocamento p = _context.OrcamentoDeslocamento.FirstOrDefault(p => p.CodOrcDeslocamento == deslocamento.CodOrcDeslocamento);

            if (p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(deslocamento);
                _context.SaveChanges();
            }
        }
    }
}
