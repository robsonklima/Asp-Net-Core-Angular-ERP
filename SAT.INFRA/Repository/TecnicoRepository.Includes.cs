﻿using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SAT.MODELS.Enums;

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
                         .Include(t => t.Autorizada)
                         .Include(t => t.Regiao);
                    break;
                default:
                    query = query
                            .Include(t => t.Filial)
                            .Include(t => t.Autorizada)
                            .Include(t => t.TipoRota)
                            .Include(t => t.Regiao)
                            .Include(t => t.Usuario)
                            .Include(t => t.Usuario.Perfil)
                            .Include(t => t.RegiaoAutorizada)
                            .Include(t => t.TecnicoCliente)
                                .ThenInclude(tc => tc.Cliente);
                    break;
            }

            return query;
        }
    }
}
