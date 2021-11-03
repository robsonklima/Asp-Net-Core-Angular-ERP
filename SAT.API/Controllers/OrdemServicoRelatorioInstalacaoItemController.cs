using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class OrdemServicoRelatorioInstalacaoItemController : ControllerBase
    {
        private readonly IOrdemServicoRelatorioInstalacaoItemService _raInsItemService;

        public OrdemServicoRelatorioInstalacaoItemController(IOrdemServicoRelatorioInstalacaoItemService raInsItemService)
        {
            _raInsItemService = raInsItemService;
        }

        [HttpGet]
        public List<OrdemServicoRelatorioInstalacaoItem> Get()
        {
            return _raInsItemService.ObterItens();
        }
    }
}
