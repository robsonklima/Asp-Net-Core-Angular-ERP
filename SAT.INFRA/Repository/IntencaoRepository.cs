using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class IntencaoRepository : IIntencaoRepository
    {
        private readonly AppDbContext _context;

        public IntencaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Intencao intencao)
        {
            Intencao inst = _context.Intencao.FirstOrDefault(i => i.CodIntencao == intencao.CodIntencao);

            if (inst != null)
            {
                _context.Entry(inst).CurrentValues.SetValues(intencao);
                _context.ChangeTracker.Clear();
                _context.SaveChanges();
            }
        }
    }
}
