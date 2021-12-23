using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class UsuarioDispositivoService : IUsuarioDispositivoService
    {
        private readonly IEmailService _emailService;
        private readonly IUsuarioDispositivoRepository _usuarioDispositivoRepo;
        private readonly IUsuarioRepository _usuarioRepo;

        public UsuarioDispositivoService(
            IEmailService emailService,
            IUsuarioDispositivoRepository usuarioDispositivoRepository,
            IUsuarioRepository usuarioRepo
        )
        {
            _emailService = emailService;
            _usuarioDispositivoRepo = usuarioDispositivoRepository;
            _usuarioRepo = usuarioRepo;
            
        }

        public void Criar(UsuarioDispositivo usuarioDispositivo)
        {
            var usuario = _usuarioRepo.ObterPorCodigo(usuarioDispositivo.CodUsuario);

            if (!string.IsNullOrWhiteSpace(usuario.Email)) {
                var email = new Email() {
                    NomeRemetente = Constants.SISTEMA_NOME,
                    EmailRemetente = Constants.SISTEMA_EMAIL,
                    NomeDestinatario = usuario.NomeUsuario,
                    EmailDestinatario = usuario.Email,
                    Assunto = Constants.SISTEMA_ATIVACAO_ACESSO,
                    //Corpo = "Clique no link a seguir para adicionar seu dispositivo https://sat.perto.com.br/SAT.V2.FRONTEND/#/default"
                    Corpo = "Clique no botão abaixo para adicionar seu novo dispositivo http://localhost:4200/#/confirmation-submit/" +
                        usuario.CodUsuario + "/" + usuarioDispositivo.Hash
                };

                _emailService.Enviar(email);
            }

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
