using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using SAT.MODELS.Entities.Constants;

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
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_DELETAR);
                }
            }
        }

        public Foto ObterPorCodigo(int codigo)
        {
            return _context.Foto.FirstOrDefault(f => f.CodRATFotoSmartphone == codigo);
        }
    }
}
