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

        [HttpGet("Acao")]
        public IActionResult ExportarAcao([FromQuery] AcaoParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.ACAO);
        }

        [HttpGet("AcaoCausa")]
        public IActionResult ExportarAcaoCausa([FromQuery] AcaoComponenteParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.ACAO_COMPONENTE);
        }

        [HttpGet("Autorizada")]
        public IActionResult ExportarAutorizada([FromQuery] AutorizadaParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.AUTORIZADA);
        }

        [HttpGet("Cidade")]
        public IActionResult ExportarCidade([FromQuery] CidadeParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.CIDADE);
        }        

        [HttpGet("Cliente")]
        public IActionResult ExportarCliente([FromQuery] ClienteParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.CLIENTE);
        }        

        [HttpGet("ClientePeca")]
        public IActionResult ExportarClientePeca([FromQuery] ClientePecaParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.CLIENTEPECA);
        }        

        [HttpGet("ClientePecaGenerica")]
        public IActionResult ExportarClientePecaGenerica([FromQuery] ClientePecaGenericaParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.CLIENTEPECAGENERICA);
        } 
        
        [HttpGet("ClienteBancada")]
        public IActionResult ExportarClienteBancada([FromQuery] ClienteBancadaParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.CLIENTEBANCADA);
        }

        [HttpGet("Contrato")]
        public IActionResult ExportarContrato([FromQuery] ContratoParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.CONTRATO);
        }                       

        [HttpGet("Defeito")]
        public IActionResult ExportarDefeito([FromQuery] DefeitoParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.DEFEITO);
        }           

        [HttpGet("Equipamento")]
        public IActionResult ExportarEquipamento([FromQuery] EquipamentoParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.EQUIPAMENTO);
        }      
        
        [HttpGet("GrupoEquipamento")]
        public IActionResult ExportarGrupoEquipamento([FromQuery] GrupoEquipamentoParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.GRUPOEQUIPAMENTO);
        }      
        
        [HttpGet("TipoEquipamento")]
        public IActionResult ExportarTipoEquipamento([FromQuery] TipoEquipamentoParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.TIPOEQUIPAMENTO);
        }   

        [HttpGet("Tecnico")]
        public IActionResult ExportarTecnico([FromQuery] TecnicoParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.TECNICO);
        }        
        [HttpGet("DefeitoComponente")]
        public IActionResult ExportarDefeitoComponente([FromQuery] DefeitoComponenteParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.DEFEITOCOMPONENTE);
        }       

        [HttpGet("EquipamentoModulo")]
        public IActionResult ExportarEquipamentoModulo([FromQuery] EquipamentoModuloParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.EQUIPAMENTOMODULO);
        }        

        [HttpGet("Feriado")]
        public IActionResult ExportarFeriado([FromQuery] FeriadoParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.FERIADO);
        }   

        [HttpGet("FerramentaTecnico")]
        public IActionResult ExportarFerramentaTecnico([FromQuery] FerramentaTecnicoParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.FERRAMENTATECNICO);
        }        

        [HttpGet("Filial")]
        public IActionResult ExportarFilial([FromQuery] FilialParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.FILIAL);
        }        

        [HttpGet("FormaPagamento")]
        public IActionResult ExportarFormaPagamento([FromQuery] FormaPagamentoParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.FORMAPAGAMENTO);
        }

        [HttpGet("LiderTecnico")]
        public IActionResult ExportarLiderTecnico([FromQuery] LiderTecnicoParameters parameters)
        {
			return _exService.Exportar(parameters, ExportacaoFormatoEnum.EXCEL, ExportacaoTipoEnum.LIDERTECNICO);
        }        
    }
}
