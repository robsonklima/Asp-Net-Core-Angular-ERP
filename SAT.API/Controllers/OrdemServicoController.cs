using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class OrdemServicoController : ControllerBase
    {
        private IOrdemServicoService _osService;
        public OrdemServicoController(IOrdemServicoService osService)
        {
            _osService = osService;
        }

        [HttpGet]
        public ListViewModel ObterPorParametros([FromQuery] OrdemServicoParameters parameters)
        {
            return _osService.ObterPorParametros(parameters);
        }

        [HttpGet("{codOS}")]
        public OrdemServico ObterPorCodigo(int codOS)
        {
            return _osService.ObterPorCodigo(codOS);
        }

        [HttpPost]
        public OrdemServico Post([FromBody] OrdemServico ordemServico)
        {
            return _osService.Criar(ordemServico);
        }

        [HttpPut]
        public OrdemServico Put([FromBody] OrdemServico ordemServico)
        {
            return _osService.Atualizar(ordemServico);
        }

        [HttpDelete("{codOS}")]
        public void Deletar(int codOS)
        {
            _osService.Deletar(codOS);
        }

        [HttpGet("export")]
        public IActionResult ExportToExcel([FromQuery] OrdemServicoParameters parameters)
        {
            return _osService.ExportToExcel(parameters);
        }
    }
}
