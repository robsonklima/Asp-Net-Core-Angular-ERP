using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace SAT.INFRA.Repository
{
    public class OrdemServicoRelatorioInstalacaoItemRepository : IOrdemServicoRelatorioInstalacaoItemRepository
    {
        private readonly AppDbContext _context;

        public OrdemServicoRelatorioInstalacaoItemRepository(AppDbContext context)
        {
            _context = context;
        }     

        public List<OrdemServicoRelatorioInstalacaoItem> ObterItens()
        {
            return _context.OrdemServicoRelatorioInstalacaoItem.ToList();
         
        }
    }
}
