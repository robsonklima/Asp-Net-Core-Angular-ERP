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
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class AuditoriaFotoController : ControllerBase
    {
        public IAuditoriaFotoService _auditoriaFotoService { get; }

        public AuditoriaFotoController(IAuditoriaFotoService auditoriaFotoService)
        {
            _auditoriaFotoService = auditoriaFotoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] AuditoriaFotoParameters parameters)
        {
            return _auditoriaFotoService.ObterPorParametros(parameters);
        }
        
        [HttpGet("{CodAuditoriaFoto}")]
        public AuditoriaFoto Get(int codAuditoriaFoto)
        {
            return _auditoriaFotoService.ObterPorCodigo(codAuditoriaFoto);
        }

        [HttpPost]
        public void Post([FromBody] AuditoriaFoto auditoriaFoto)
        {
            _auditoriaFotoService.Criar(auditoriaFoto);
        }

        [HttpPut]
        public void Put([FromBody] AuditoriaFoto auditoriaFoto)
        {
            _auditoriaFotoService.Atualizar(auditoriaFoto);
        }

        [HttpDelete("{CodAuditoriaFoto}")]
        public void Delete(int codAuditoriaFoto)
        {
            _auditoriaFotoService.Deletar(codAuditoriaFoto);
        }
    }
}
