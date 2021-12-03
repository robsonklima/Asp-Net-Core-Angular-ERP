using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SAT.MODELS.Enums;
using System;

namespace SAT.INFRA.Repository
{
    public partial class TecnicoRepository : ITecnicoRepository
    {
        public IQueryable<Tecnico> AplicarIncludes(IQueryable<Tecnico> query, TecnicoIncludeEnum include)
        {
            switch (include)
            {
                case TecnicoIncludeEnum.TECNICO_ORDENS_SERVICO:
                    query = query
                         .Include(t => t.Filial)
                            .Include(t => t.Usuario)
                                .ThenInclude(t => t.PontosUsuario)
                            .Include(t => t.OrdensServico)
                                .ThenInclude(t => t.RelatoriosAtendimento);
                    break;
                default:
                    query = query
                            .Include(t => t.Filial)
                            .Include(t => t.Autorizada)
                            .Include(t => t.TipoRota)
                            .Include(t => t.Regiao)
                            .Include(t => t.Usuario)
                                .ThenInclude(t => t.PontosUsuario
                                .Where(i => i.DataHoraRegistro.Date == DateTime.Today.Date))
                            .Include(t => t.RegiaoAutorizada);
                    break;
            }

            return query;
        }
    }
}
