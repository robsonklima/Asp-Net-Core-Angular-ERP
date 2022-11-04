using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Views;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;

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
        public ListViewModel Get([FromQuery] BancadaLaboratorioParameters parameters)
        {
            return _bancadaLaboratorioSvc.ObterPorParametros(parameters);
        }

        [HttpGet("{numBancada}")]
        public BancadaLaboratorio Get(int numBancada)
        {
            return _bancadaLaboratorioSvc.ObterPorCodigo(numBancada);
        }

        [HttpPost]
        public BancadaLaboratorio Post([FromBody] BancadaLaboratorio lab)
        {
            return _bancadaLaboratorioSvc.Criar(lab);
        }

        [HttpPut]
        public void Put([FromBody] BancadaLaboratorio lab)
        {
            _bancadaLaboratorioSvc.Atualizar(lab);
        }

        [HttpDelete("{numBancada}")]
        public void Delete(int numBancada)
        {
            _bancadaLaboratorioSvc.Deletar(numBancada);
        }

        [HttpGet("TecnicosBancada")]
        public List<ViewLaboratorioTecnicoBancada> ObterTecnicosBancada([FromQuery] BancadaLaboratorioParameters parameters) =>
            _bancadaLaboratorioSvc.ObterPorView(parameters);
    }
}
