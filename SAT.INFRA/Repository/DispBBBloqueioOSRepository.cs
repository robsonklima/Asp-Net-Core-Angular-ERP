using SAT.INFRA.Context;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class DispBBBloqueioOSRepository : IDispBBBloqueioOSRepository
    {
        private readonly AppDbContext _context;

        public DispBBBloqueioOSRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(DispBBBloqueioOS dispBBBloqueioOS)
        {
            throw new NotImplementedException();
        }

        public void Criar(DispBBBloqueioOS dispBBBloqueioOS)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new NotImplementedException();
        }

        public DispBBBloqueioOS ObterPorCodigo(int codigo)
        {
            throw new NotImplementedException();
        }

        public PagedList<DispBBBloqueioOS> ObterPorParametros(DispBBBloqueioOSParameters parameters)
        {
            var dispBBBloqueioOS = _context.DispBBBloqueioOS.AsQueryable();

             if (parameters.CodOS.HasValue)
                dispBBBloqueioOS = dispBBBloqueioOS.Where(d => d.CodOS == parameters.CodOS);

             if (parameters.IndAtivo.HasValue)
                dispBBBloqueioOS = dispBBBloqueioOS.Where(d => d.IndAtivo == parameters.IndAtivo);

             return PagedList<DispBBBloqueioOS>.ToPagedList(dispBBBloqueioOS, parameters.PageNumber, parameters.PageSize);

        }
    }
}