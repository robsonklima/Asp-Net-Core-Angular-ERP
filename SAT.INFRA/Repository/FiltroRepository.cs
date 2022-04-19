using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class FiltroRepository : IFiltroRepository
    {
        private readonly AppDbContext _context;

        public FiltroRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(FiltroUsuario filtroUsuario)
        {
            _context.ChangeTracker.Clear();
            FiltroUsuario data = _context.FiltroUsuario.SingleOrDefault(a => a.CodFiltroUsuario == filtroUsuario.CodFiltroUsuario);

            if (data != null)
            {
                try
                {
                    _context.Entry(data).CurrentValues.SetValues(filtroUsuario);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public void Criar(FiltroUsuario filtroUsuario)
        {
            try
            {
                _context.Add(filtroUsuario);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            FiltroUsuario data = _context.FiltroUsuario.SingleOrDefault(a => a.CodFiltroUsuario == codigo);

            if (data != null)
            {
                try
                {
                    _context.FiltroUsuario.Remove(data);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public FiltroUsuario ObterPorCodigo(int codigo)
        {
            return _context.FiltroUsuario.SingleOrDefault(a => a.CodFiltroUsuario == codigo);
        }
    }
}
