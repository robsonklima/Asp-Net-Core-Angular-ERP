using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoServicoController : ControllerBase
    {
        private readonly ITipoServicoService _tipoServicoService;

        public TipoServicoController(
            ITipoServicoService tipoServicoService
        )
        {
            _tipoServicoService = tipoServicoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] TipoServicoParameters parameters)
        {
            return _tipoServicoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTipoServico}")]
        public TipoServico Get(int codTipoServico)
        {
            return _tipoServicoService.ObterPorCodigo(codTipoServico);
        }

        [HttpPost]
        public void Post([FromBody] TipoServico tipoServico)
        {
            _tipoServicoService.Criar(tipoServico);
        }

        [HttpPut]
        public void Put([FromBody] TipoServico tipoServico)
        {
            _tipoServicoService.Atualizar(tipoServico);
        }

        [HttpDelete("{codServico}")]
        public void Delete(int codServico)
        {
            _tipoServicoService.Deletar(codServico);
        }
    }
}
