using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioLoginController : ControllerBase
    {
        private readonly IUsuarioLoginService _usuarioLoginService;

        public UsuarioLoginController(IUsuarioLoginService UsuarioLoginService)
        {
            _usuarioLoginService = UsuarioLoginService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] UsuarioLoginParameters parameters)
        {
            return _usuarioLoginService.ObterPorParametros(parameters);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] UsuarioLogin login)
        {
            _usuarioLoginService.Criar(login);
        }
    }
}
