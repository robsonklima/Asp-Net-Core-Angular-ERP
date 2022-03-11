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
    public class ImportacaoConfiguracaoController : ControllerBase
    {
        private readonly IImportacaoConfiguracaoService _importacaoConfiguracaoService;

        public ImportacaoConfiguracaoController(IImportacaoConfiguracaoService importacaoConfiguracaoService)
        {
            _importacaoConfiguracaoService = importacaoConfiguracaoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ImportacaoConfiguracaoParameters parameters)
        {
            return _importacaoConfiguracaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codImportacaoConf}")]
        public ImportacaoConfiguracao Get(int codImportacaoConf)
        {
            return _importacaoConfiguracaoService.ObterPorCodigo(codImportacaoConf);
        }

        [HttpPost]
        public ImportacaoConfiguracao Post([FromBody] ImportacaoConfiguracao importacaoConf)
        {
            return _importacaoConfiguracaoService.Criar(importacaoConf);
        }

        [HttpPut]
        public void Put([FromBody] ImportacaoConfiguracao importacaoConf)
        {
            _importacaoConfiguracaoService.Atualizar(importacaoConf);
        }

        [HttpDelete("{codImportacaoConf}")]
        public void Delete(int codImportacaoConf)
        {
            _importacaoConfiguracaoService.Deletar(codImportacaoConf);
        }
    }
}
