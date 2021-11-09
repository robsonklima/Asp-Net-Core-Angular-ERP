using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SAT.MODELS.Enums;

namespace SAT.INFRA.Repository
{
    public partial class FilialRepository : IFilialRepository
    {
        public IQueryable<Filial> AplicarIncludes(IQueryable<Filial> query, FilialIncludeEnum include)
        {
            switch (include)
            {
                case FilialIncludeEnum.FILIAL_ORDENS_SERVICO:
                    query = query.Include(i => i.Cidade)
                             .Include(i => i.Cidade.UnidadeFederativa)
                             .Include(i => i.OrdensServico)
                                 .ThenInclude(os => os.EquipamentoContrato)
                                 .ThenInclude(os => os.Equipamento);
                    break;
                default:
                    query = query.Include(i => i.Cidade)
                            .Include(i => i.Cidade.UnidadeFederativa);
                    break;
            }

            return query;
        }
    }
}
