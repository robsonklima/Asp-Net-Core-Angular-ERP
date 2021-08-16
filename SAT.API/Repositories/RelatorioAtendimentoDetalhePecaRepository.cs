using Microsoft.EntityFrameworkCore;
using SAT.API.Context;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using System;
using System.Linq;

namespace SAT.API.Repositories
{
    public class RelatorioAtendimentoDetalhePecaRepository : IRelatorioAtendimentoDetalhePecaRepository
    {
        private readonly AppDbContext _context;
        public RelatorioAtendimentoDetalhePecaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Criar(RelatorioAtendimentoDetalhePeca detalhePeca)
        {
            try
            {
                _context.Add(detalhePeca);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Deletar(int codRATDetalhePeca)
        {
            RelatorioAtendimentoDetalhePeca dp = _context.RelatorioAtendimentoDetalhePeca
                .FirstOrDefault(dp => dp.CodRATDetalhePeca == codRATDetalhePeca);

            if (dp != null)
            {
                _context.RelatorioAtendimentoDetalhePeca.Remove(dp);
                _context.SaveChanges();
            }
        }

        public RelatorioAtendimentoDetalhePeca ObterPorCodigo(int codigo)
        {
            return _context.RelatorioAtendimentoDetalhePeca
                .SingleOrDefault(r => r.CodRATDetalhePeca == codigo);
        }
    }
}
