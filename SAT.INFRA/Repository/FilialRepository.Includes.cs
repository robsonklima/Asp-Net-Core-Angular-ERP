using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
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
                    query = query
                             .Include(i => i.Cidade!)
                                .DefaultIfEmpty()
                             .Include(i => i.Cidade.UnidadeFederativa)
                             .Include(i => i.OrdensServico)
                                 .ThenInclude(os => os.EquipamentoContrato)
                                 .ThenInclude(os => os.Equipamento);
                    break;
                default:
                    query = query
                            .Include(i => i.Cidade!)
                                .DefaultIfEmpty()
                            .Include(i => i.Cidade.UnidadeFederativa);
                    break;
            }

            return query;
        }
    }
}
