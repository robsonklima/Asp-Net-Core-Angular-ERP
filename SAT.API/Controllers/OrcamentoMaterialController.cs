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
    public class OrcamentoMaterialController : ControllerBase
    {
        private readonly IOrcamentoMaterialService _orcamentoMatService;

        public OrcamentoMaterialController(IOrcamentoMaterialService orcamentoMatService)
        {
            _orcamentoMatService = orcamentoMatService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] OrcamentoMaterialParameters parameters) =>
            _orcamentoMatService.ObterPorParametros(parameters);

        [HttpGet("{codOrcamentoMat}")]
        public OrcamentoMaterial Get(int codOrcamentoMat) =>
            _orcamentoMatService.ObterPorCodigo(codOrcamentoMat);

        [HttpPost]
        public OrcamentoMaterial Post([FromBody] OrcamentoMaterial orcamentoMat) =>
            _orcamentoMatService.Criar(orcamentoMat);

        [HttpPut]
        public OrcamentoMaterial Put([FromBody] OrcamentoMaterial orcamentoMat) =>
            _orcamentoMatService.Atualizar(orcamentoMat);

        [HttpDelete("{codOrcamentoMat}")]
        public void Delete(int codOrcamentoMat) =>
            _orcamentoMatService.Deletar(codOrcamentoMat);
    }
}