using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
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
            catch (Exception ex)
            {
                throw new Exception($"", ex);
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
