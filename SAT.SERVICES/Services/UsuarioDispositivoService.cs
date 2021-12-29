using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
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

        public ListViewModel ObterPorParametros(UsuarioDispositivoParameters parameters)
        {
            var dispositivos = _usuarioDispositivoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = dispositivos,
                TotalCount = dispositivos.TotalCount,
                CurrentPage = dispositivos.CurrentPage,
                PageSize = dispositivos.PageSize,
                TotalPages = dispositivos.TotalPages,
                HasNext = dispositivos.HasNext,
                HasPrevious = dispositivos.HasPrevious
            };

            return lista;
        }

        public UsuarioDispositivo ObterPorCodigo(int codigo)
        {
            return _usuarioDispositivoRepo.ObterPorCodigo(codigo);
        }

        public UsuarioDispositivo Criar(UsuarioDispositivo usuarioDispositivo)
        {
            return _usuarioDispositivoRepo.Criar(usuarioDispositivo);
        }

        public void Atualizar(UsuarioDispositivo usuarioDispositivo)
        {
            _usuarioDispositivoRepo.Atualizar(usuarioDispositivo);
        }
    }
}
