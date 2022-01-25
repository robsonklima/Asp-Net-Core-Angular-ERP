using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class RelatorioAtendimentoDetalheRepository : IRelatorioAtendimentoDetalheRepository
    {
        private readonly AppDbContext _context;
        public RelatorioAtendimentoDetalheRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(RelatorioAtendimentoDetalhe detalhe)
        {
            RelatorioAtendimentoDetalhe d = _context.RelatorioAtendimentoDetalhe
                .FirstOrDefault(d => d.CodRATDetalhe == detalhe.CodRATDetalhe);

            if (d != null)
            {
                detalhe.RelatorioAtendimentoDetalhePecas = null;
                _context.Entry(d).CurrentValues.SetValues(detalhe);
                _context.ChangeTracker.Clear();
                _context.SaveChanges();
            }
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
