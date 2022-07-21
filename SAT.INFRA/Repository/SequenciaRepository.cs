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
            _context.ChangeTracker.Clear();
            var sequencia = _context.Sequencia.FirstOrDefault(s => s.Tabela == tabela);

            if (sequencia == null)
            {
                throw new Exception("Sequência não encontrada para esta tabela");
            }

            sequencia.Contador = sequencia.Contador + 1;
            _context.SaveChanges();

            return sequencia.Contador;
        }

        public int AtualizaContadorOS(int total)
        {
            _context.ChangeTracker.Clear();
            var sequencia = _context.Sequencia.FirstOrDefault(s => s.Tabela == "OS");

            var inicial = sequencia.Contador;

            sequencia.Contador = sequencia.Contador + total + 10;

            _context.SaveChanges();

            return inicial;
        }
    }
}
