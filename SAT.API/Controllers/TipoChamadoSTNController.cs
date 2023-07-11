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
    public class TipoChamadoSTNController : ControllerBase
    {
        public ITipoChamadoSTNService _tipoChamadoSTNService { get; }

        public TipoChamadoSTNController(ITipoChamadoSTNService tipoChamadoSTNService)
        {
            _tipoChamadoSTNService = tipoChamadoSTNService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] TipoChamadoSTNParameters parameters)
        {
            return _tipoChamadoSTNService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodTipoChamadoSTN}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public TipoChamadoSTN Get(int codTipoChamadoSTN)
        {
            return _tipoChamadoSTNService.ObterPorCodigo(codTipoChamadoSTN);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] TipoChamadoSTN tipoChamadoSTN)
        {
            _tipoChamadoSTNService.Criar(tipoChamadoSTN);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] TipoChamadoSTN tipoChamadoSTN)
        {
            _tipoChamadoSTNService.Atualizar(tipoChamadoSTN);
        }

        [HttpDelete("{CodTipoChamadoSTN}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codTipoChamadoSTN)
        {
            _tipoChamadoSTNService.Deletar(codTipoChamadoSTN);
        }
    }
}
