using System.Security.Claims;
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
    public class FilialController : ControllerBase
    {
        private readonly IFilialService _filialService;

        public FilialController(IFilialService filialService)
        {
            _filialService = filialService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] FilialParameters parameters)
        {
            return _filialService.ObterPorParametros(parameters);
        }

        [HttpGet("{codFilial}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Filial Get(int codFilial)
        {
            return _filialService.ObterPorCodigo(codFilial);
        }


        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public Filial Post([FromBody] Filial filial)
        {
            return this._filialService.Criar(filial);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] Filial filial)
        {
            this._filialService.Atualizar(filial);
        }

        [HttpDelete("{codFilial}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codFilial)
        {
            this._filialService.Deletar(codFilial);
        }
    }
}
