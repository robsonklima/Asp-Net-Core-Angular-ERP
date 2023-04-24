using Microsoft.AspNetCore.Http;
using NLog;
using NLog.Fluent;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;

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
            var msg = $"MESSAGE: {log.Message} - " +
              $"FILE: {log?.FileName} - " +
              $"LEVEL: {log?.Level} - " +
              $"LINENUMBER: {log?.LineNumber} - " +
              $"TIMESTAMP: {log?.Timestamp:F}";

            _logger.Error()
                .Message($"{ msg }")
                .Property("application", Constants.SISTEMA_NOME)
                .Property("layer", Constants.SISTEMA_CAMADA_FE)
                .Property("user", _contextAcecssor.HttpContext.User.Identity.Name)
                .Write();
        }
    }
}
