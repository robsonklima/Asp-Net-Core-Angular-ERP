using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class UsuarioDispositivoRepository : IUsuarioDispositivoRepository
    {
        private readonly AppDbContext _context;

        public UsuarioDispositivoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(UsuarioDispositivo usuarioDispositivo)
        {
            UsuarioDispositivo d = _context.UsuarioDispositivo
                .OrderByDescending(d => d.DataHoraCad)
                .FirstOrDefault(d => d.CodUsuario == usuarioDispositivo.CodUsuario && d.Hash == usuarioDispositivo.Hash);

            if (d != null)
            {
                _context.Entry(d).CurrentValues.SetValues(usuarioDispositivo);
                _context.SaveChanges();
            }
        }

        public void Criar(UsuarioDispositivo usuarioDispositivo)
        {
            _context.Add(usuarioDispositivo);
            _context.SaveChanges();
        }

        public UsuarioDispositivo ObterPorUsuarioEHash(string codUsuario, string hash)
        {
            return _context.UsuarioDispositivo
                .OrderByDescending(h => h.DataHoraCad)
                .FirstOrDefault(h => h.CodUsuario == codUsuario && h.Hash == hash);
        }
    }
}
