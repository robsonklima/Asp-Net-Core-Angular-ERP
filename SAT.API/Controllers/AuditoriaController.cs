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
    public class AuditoriaController : ControllerBase
    {
        public IAuditoriaService _auditoriaService { get; }

        public AuditoriaController(IAuditoriaService auditoriaService)
        {
            _auditoriaService = auditoriaService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] AuditoriaParameters parameters)
        {
            return _auditoriaService.ObterPorParametros(parameters);
        }
        
        [HttpGet("{CodAuditoria}")]
        public Auditoria Get(int codAuditoria)
        {
            return _auditoriaService.ObterPorCodigo(codAuditoria);
        }

        [HttpPost]
        public void Post([FromBody] Auditoria auditoria)
        {
            _auditoriaService.Criar(auditoria);
        }

        [HttpPut]
        public void Put([FromBody] Auditoria auditoria)
        {
            _auditoriaService.Atualizar(auditoria);
        }

        [HttpDelete("{CodAuditoria}")]
        public void Delete(int codAuditoria)
        {
            _auditoriaService.Deletar(codAuditoria);
        }
    }
}
