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
    [ApiController]
    [Route("api/[controller]")]
    public class StatusServicoSTNController : ControllerBase
    {
        private readonly IStatusServicoSTNService _statusServicoSTNService;

        public StatusServicoSTNController(IStatusServicoSTNService statusServicoSTNService)
        {
            _statusServicoSTNService = statusServicoSTNService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] StatusServicoSTNParameters parameters)
        {
            return _statusServicoSTNService.ObterPorParametros(parameters);
        }

        [HttpGet("{codStatusServicoSTN}")]
        public StatusServicoSTN Get(int codStatusServicoSTN)
        {
            return _statusServicoSTNService.ObterPorCodigo(codStatusServicoSTN);
        }

        [HttpPost]
        public StatusServicoSTN Post([FromBody] StatusServicoSTN statusServicoSTN)
        {
            return _statusServicoSTNService.Criar(statusServicoSTN);
        }

        [HttpPut]
        public void Put([FromBody] StatusServicoSTN StatusServicoSTN)
        {
            _statusServicoSTNService.Atualizar(StatusServicoSTN);
        }

        [HttpDelete("{codStatusServicoSTN:int}")]
        public void Delete(int codStatusServicoSTN)
        {
            _statusServicoSTNService.Deletar(codStatusServicoSTN);
        }
    }
}
