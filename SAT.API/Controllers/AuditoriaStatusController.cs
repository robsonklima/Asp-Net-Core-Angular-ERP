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
    public class AuditoriaStatusController : ControllerBase
    {
        public IAuditoriaStatusService _auditoriaStatusService { get; }

        public AuditoriaStatusController(IAuditoriaStatusService auditoriaStatusService)
        {
            _auditoriaStatusService = auditoriaStatusService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] AuditoriaStatusParameters parameters)
        {
            return _auditoriaStatusService.ObterPorParametros(parameters);
        }
        
        [HttpGet("{CodAuditoriaStatus}")]
        public AuditoriaStatus Get(int codAuditoriaStatus)
        {
            return _auditoriaStatusService.ObterPorCodigo(codAuditoriaStatus);
        }

        [HttpPost]
        public void Post([FromBody] AuditoriaStatus auditoriaStatus)
        {
            _auditoriaStatusService.Criar(auditoriaStatus);
        }

        [HttpPut]
        public void Put([FromBody] AuditoriaStatus auditoriaStatus)
        {
            _auditoriaStatusService.Atualizar(auditoriaStatus);
        }

        [HttpDelete("{CodAuditoriaStatus}")]
        public void Delete(int codAuditoriaStatus)
        {
            _auditoriaStatusService.Deletar(codAuditoriaStatus);
        }
    }
}
