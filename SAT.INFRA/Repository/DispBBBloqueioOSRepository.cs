using SAT.INFRA.Context;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;

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
            throw new NotImplementedException();
        }
    }
}