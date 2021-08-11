using SAT.API.Context;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
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
