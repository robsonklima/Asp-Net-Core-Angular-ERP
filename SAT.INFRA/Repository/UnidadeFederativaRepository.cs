using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class UnidadeFederativaRepository : IUnidadeFederativaRepository
    {
        private readonly AppDbContext _context;

        public UnidadeFederativaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(UnidadeFederativa uf)
        {
            UnidadeFederativa unid = _context.UnidadeFederativa.FirstOrDefault(p => p.CodUF == uf.CodUF);

            if (unid != null)
            {
                _context.Entry(unid).CurrentValues.SetValues(uf);
                _context.SaveChanges();
            }
        }

        public void Criar(UnidadeFederativa uf)
        {
            _context.Add(uf);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            UnidadeFederativa unid = _context.UnidadeFederativa.FirstOrDefault(uf => uf.CodUF == codigo);

            if (unid != null)
            {
                _context.UnidadeFederativa.Remove(unid);
                _context.SaveChanges();
            }
        }

        public UnidadeFederativa ObterPorCodigo(int codigo)
        {
            return _context.UnidadeFederativa.FirstOrDefault(uf => uf.CodUF == codigo);
        }

        public PagedList<UnidadeFederativa> ObterPorParametros(UnidadeFederativaParameters parameters)
        {
            var ufs = _context.UnidadeFederativa
                .Include(uf => uf.Pais)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                ufs = ufs.Where(
                            uf =>
                            uf.SiglaUF.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            uf.NomeUF.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            uf.CodUF.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodUF != null)
            {
                ufs = ufs.Where(uf => uf.CodUF == parameters.CodUF);
            }

			if (parameters.CodPais != null)
            {
                ufs = ufs.Where(uf => uf.CodPais == parameters.CodPais);
            }            

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                ufs = ufs.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<UnidadeFederativa>.ToPagedList(ufs, parameters.PageNumber, parameters.PageSize);
        }
    }
}
