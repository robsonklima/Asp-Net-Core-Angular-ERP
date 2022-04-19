using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Repository
{
    public partial class FotoRepository : IFotoRepository
    {
        private readonly AppDbContext _context;

        public FotoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Criar(Foto foto)
        {
            _context.Add(foto);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            Foto f = _context.Foto.SingleOrDefault(f => f.CodRATFotoSmartphone == codigo);

            if (f != null)
            {
                _context.Foto.Remove(f);

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public Foto ObterPorCodigo(int codigo)
        {
            return _context.Foto.FirstOrDefault(f => f.CodRATFotoSmartphone == codigo);
        }

        public PagedList<Foto> ObterPorParametros(FotoParameters parameters)
        {
            var fotos = _context.Foto.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.NumRAT))
                fotos = fotos.Where(f => f.NumRAT == parameters.NumRAT);

            if (parameters.CodOS.HasValue)
                fotos = fotos.Where(f => f.CodOS == parameters.CodOS);

            if (!string.IsNullOrWhiteSpace(parameters.SortActive) && !string.IsNullOrWhiteSpace(parameters.SortDirection))
                fotos = fotos.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<Foto>.ToPagedList(fotos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
