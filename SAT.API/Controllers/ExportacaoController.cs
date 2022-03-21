using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class ExportacaoController : ControllerBase
    {
        private IExportacaoService _exService;
        public ExportacaoController(IExportacaoService exService)
        {
			_exService = exService;
        }
		
        [HttpGet("OrdemServico")]
        public IActionResult ExportarOrdemServico([FromQuery] OrdemServicoParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.ORDEM_SERVICO);
        }

        [HttpGet("EquipamentoContrato")]
        public IActionResult ExportarEquipamentoContrato([FromQuery] EquipamentoContratoParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.EQUIPAMENTO_CONTRATO);
        }
    }
}
