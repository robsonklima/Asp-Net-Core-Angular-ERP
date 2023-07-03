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
    public class ANSController : ControllerBase
    {
        public IANSService _ansService { get; }

        public ANSController(IANSService ansService)
        {
            _ansService = ansService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ANSParameters parameters)
        {
            return _ansService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodANS}")]
        public ANS Get(int CodANS)
        {
            return _ansService.ObterPorCodigo(CodANS);
        }

        [HttpPost]
        public void Post([FromBody] ANS ans)
        {
            _ansService.Criar(ans);
        }

        [HttpPut]
        public void Put([FromBody] ANS ans)
        {
            _ansService.Atualizar(ans);
        }

        [HttpDelete("{CodANS}")]
        public void Delete(int CodANS)
        {
            _ansService.Deletar(CodANS);
        }
    }
}
