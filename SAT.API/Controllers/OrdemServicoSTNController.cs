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
    public class OrdemServicoSTNController : ControllerBase
    {
        private readonly IOrdemServicoSTNService _ordemServicoSTNService;

        public OrdemServicoSTNController(IOrdemServicoSTNService ordemServicoSTNService)
        {
            _ordemServicoSTNService = ordemServicoSTNService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] OrdemServicoSTNParameters parameters)
        {
            return _ordemServicoSTNService.ObterPorParametros(parameters);
        }

        [HttpGet("{codAtendimento}")]
        public OrdemServicoSTN Get(int codAtendimento)
        {
            return _ordemServicoSTNService.ObterPorCodigo(codAtendimento);
        }

        [HttpPost]
        public OrdemServicoSTN Post([FromBody] OrdemServicoSTN ordem)
        {
            return _ordemServicoSTNService.Criar(ordem);
        }

        [HttpPut]
        public OrdemServicoSTN Put([FromBody] OrdemServicoSTN ordem)
        {
            return _ordemServicoSTNService.Atualizar(ordem);
        }

        [HttpDelete("{codAtendimento}")]
        public void Delete(int codAtendimento)
        {
            _ordemServicoSTNService.Deletar(codAtendimento);
        }
    }
}
