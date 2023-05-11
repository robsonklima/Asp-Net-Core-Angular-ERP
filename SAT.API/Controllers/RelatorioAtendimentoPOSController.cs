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
    public class RelatorioAtendimentoPOSController : ControllerBase
    {
        private readonly IRelatorioAtendimentoPOSService _relatorioAtendimentoPOSService;

        public RelatorioAtendimentoPOSController(IRelatorioAtendimentoPOSService relatorioAtendimentoPOSService)
        {
            this._relatorioAtendimentoPOSService = relatorioAtendimentoPOSService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] RelatorioAtendimentoPOSParameters parameters)
        {
            return _relatorioAtendimentoPOSService.ObterPorParametros(parameters);
        }

        [HttpPost]
        public RelatorioAtendimentoPOS Post([FromBody] RelatorioAtendimentoPOS RelatorioAtendimentoPOS)
        {
            return _relatorioAtendimentoPOSService.Criar(RelatorioAtendimentoPOS);
        }

        [HttpPut]
        public RelatorioAtendimentoPOS Put([FromBody] RelatorioAtendimentoPOS RelatorioAtendimentoPOS)
        {
            return _relatorioAtendimentoPOSService.Atualizar(RelatorioAtendimentoPOS);
        }

        [HttpDelete("{codRATBanrisul}")]
        public RelatorioAtendimentoPOS Delete(int codRATBanrisul)
        {
            return _relatorioAtendimentoPOSService.Deletar(codRATBanrisul);
        }

        [HttpGet("{codRATBanrisul}")]
        public RelatorioAtendimentoPOS Get(int codRATBanrisul)
        {
            return _relatorioAtendimentoPOSService.ObterPorCodigo(codRATBanrisul);
        }

    }
}
