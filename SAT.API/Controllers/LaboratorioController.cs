using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Views;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class LaboratorioController : ControllerBase
    {
        private ILaboratorioService _laboratorioService;
        public LaboratorioController(ILaboratorioService laboratorioService)
        {
            _laboratorioService = laboratorioService;
        }

        [HttpGet("TecnicosBancada")]
        public List<ViewLaboratorioTecnicoBancada> ObterTecnicosBancada() =>
            _laboratorioService.ObterTecnicosBancada();
    }
}