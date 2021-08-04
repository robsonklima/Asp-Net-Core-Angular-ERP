using SAT.API.Context;
using SAT.API.Repositories.Interfaces;
using System;
using System.Linq;

namespace SAT.API.Repositories
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
            _context.SaveChanges();

            return sequencia.Contador;
        }
    }
}
