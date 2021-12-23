using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class UsuarioDispositivoService : IUsuarioDispositivoService
    {
        private readonly IUsuarioDispositivoRepository _usuarioDispositivoRepo;
        
        public UsuarioDispositivoService(
            IEmailService emailService,
            IUsuarioDispositivoRepository usuarioDispositivoRepository,
            IUsuarioRepository usuarioRepo
        )
        {
            _usuarioDispositivoRepo = usuarioDispositivoRepository;    
        }

        public void Criar(UsuarioDispositivo usuarioDispositivo)
        {
            _usuarioDispositivoRepo.Criar(usuarioDispositivo);
        }

        public void Atualizar(UsuarioDispositivo usuarioDispositivo)
        {
            _usuarioDispositivoRepo.Atualizar(usuarioDispositivo);
        }

        public UsuarioDispositivo ObterPorUsuarioEHash(string codUsuario, string hash)
        {
            return _usuarioDispositivoRepo.ObterPorUsuarioEHash(codUsuario, hash);
        }
    }
}
