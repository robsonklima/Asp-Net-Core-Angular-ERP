using NLog;
using NLog.Fluent;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;
using Microsoft.AspNetCore.Http;

namespace SAT.SERVICES.Services
{
    public class LogFrontEndService : ILogFrontEndService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IHttpContextAccessor _contextAcecssor;

        public LogFrontEndService(
            IUsuarioRepository usuarioRepo,
            IHttpContextAccessor contextAcecssor
        ) {
            _usuarioRepo = usuarioRepo;
            _contextAcecssor = contextAcecssor;
        }
    
        public void Criar(LogFrontEnd log)
        {
            var msg = $"{log.Message}, {log?.FileName}, Level: {log?.Level}, Line: {log?.LineNumber}";

            _logger.Error()
                .Message($"{msg}")
                .Property("application", Constants.SISTEMA_CAMADA_FE)
                .Property("layer", Constants.SISTEMA_CAMADA_FE)
                .Property("user", _contextAcecssor.HttpContext.User.Identity.Name)
                .Write();
        }
    }
}
