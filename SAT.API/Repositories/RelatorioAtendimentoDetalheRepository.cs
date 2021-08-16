using Microsoft.EntityFrameworkCore;
using SAT.API.Context;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using System;
using System.Linq;

namespace SAT.API.Repositories
{
    public class RelatorioAtendimentoDetalheRepository : IRelatorioAtendimentoDetalheRepository
    {
        private readonly AppDbContext _context;
        public RelatorioAtendimentoDetalheRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Criar(RelatorioAtendimentoDetalhe detalhe)
        {
            try
            {
                _context.Add(detalhe);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Deletar(int codRATDetalhe)
        {
            RelatorioAtendimentoDetalhe detalhe = _context.RelatorioAtendimentoDetalhe
                .FirstOrDefault(detalhe => detalhe.CodRATDetalhe == codRATDetalhe);

            if (detalhe != null)
            {
                _context.RelatorioAtendimentoDetalhe.Remove(detalhe);
                _context.SaveChanges();
            }
        }

        public RelatorioAtendimentoDetalhe ObterPorCodigo(int codigo)
        {
            return _context.RelatorioAtendimentoDetalhe.SingleOrDefault(r => r.CodRATDetalhe == codigo);
        }
    }
}
