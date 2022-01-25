using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class SequenciaRepository : ISequenciaRepository
    {
        private readonly AppDbContext _context;

        public SequenciaRepository(AppDbContext context)
        {
            _context = context;
        }

        public int ObterContador(string tabela)
        {
            var sequencia = _context.Sequencia.FirstOrDefault(s => s.Tabela == tabela);

            if (sequencia == null)
            {
                throw new Exception();
            }

            sequencia.Contador = sequencia.Contador + 1;
            _context.ChangeTracker.Clear();
            _context.SaveChanges();

            return sequencia.Contador;
        }
    }
}
