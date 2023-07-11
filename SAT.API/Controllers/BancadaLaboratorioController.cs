using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Views;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using System.Security.Claims;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class BancadaLaboratorioController : ControllerBase
    {
        private readonly IBancadaLaboratorioService _bancadaLaboratorioSvc;

        public BancadaLaboratorioController(IBancadaLaboratorioService bancadaLaboratorioService)
        {
            _bancadaLaboratorioSvc = bancadaLaboratorioService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] BancadaLaboratorioParameters parameters)
        {
            return _bancadaLaboratorioSvc.ObterPorParametros(parameters);
        }

        [HttpGet("{numBancada}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public BancadaLaboratorio Get(int numBancada)
        {
            return _bancadaLaboratorioSvc.ObterPorCodigo(numBancada);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public BancadaLaboratorio Post([FromBody] BancadaLaboratorio lab)
        {
            return _bancadaLaboratorioSvc.Criar(lab);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] BancadaLaboratorio lab)
        {
            _bancadaLaboratorioSvc.Atualizar(lab);
        }

        [HttpDelete("{numBancada}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int numBancada)
        {
            _bancadaLaboratorioSvc.Deletar(numBancada);
        }

        [HttpGet("TecnicosBancada")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public List<ViewLaboratorioTecnicoBancada> ObterTecnicosBancada([FromQuery] BancadaLaboratorioParameters parameters) =>
            _bancadaLaboratorioSvc.ObterPorView(parameters);
    }
}
