using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    //[Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class ImportacaoTipoController : ControllerBase
    {
        private readonly IImportacaoTipoService _importacaoTipoService;

        public ImportacaoTipoController(IImportacaoTipoService importacaoTipoService)
        {
            _importacaoTipoService = importacaoTipoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ImportacaoTipoParameters parameters)
        {
            return _importacaoTipoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codImportacaoConf}")]
        public ImportacaoTipo Get(int codImportacaoTipo)
        {
            return _importacaoTipoService.ObterPorCodigo(codImportacaoTipo);
        }

        [HttpPost]
        public ImportacaoTipo Post([FromBody] ImportacaoTipo importacaoTipo)
        {
            return _importacaoTipoService.Criar(importacaoTipo);
        }

        [HttpPut]
        public void Put([FromBody] ImportacaoTipo importacaoTipo)
        {
            _importacaoTipoService.Atualizar(importacaoTipo);
        }

        [HttpDelete("{codImportacaoTipo}")]
        public void Delete(int codImportacaoTipo)
        {
            _importacaoTipoService.Deletar(codImportacaoTipo);
        }
    }
}
