using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Repository
{
    public class UsuarioLoginRepository : IUsuarioLoginRepository
    {
        private readonly AppDbContext _context;

        public UsuarioLoginRepository(AppDbContext context)
        {
            _context = context;
        }

        public UsuarioLogin Criar(UsuarioLogin login)
        {
            _context.Add(login);
            _context.SaveChanges();
            return login;
        }
    }
}
