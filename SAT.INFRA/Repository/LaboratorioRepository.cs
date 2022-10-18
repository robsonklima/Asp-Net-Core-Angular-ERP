using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq;
using System.Collections.Generic;
using SAT.MODELS.Views;

namespace SAT.INFRA.Repository
{
    public partial class LaboratorioRepository : ILaboratorioRepository
    {
        private readonly AppDbContext _context;

        public LaboratorioRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<ViewLaboratorioTecnicoBancada> ObterTecnicosBancada()
        {
            return _context.ViewLaboratorioTecnicoBancada.ToList();
        }
    }
}
