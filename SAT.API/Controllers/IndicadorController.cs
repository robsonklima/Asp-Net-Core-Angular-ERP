using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class IndicadorController : ControllerBase
    {
        private readonly IIndicadorService _indicadorService;

        public IndicadorController(IIndicadorService indicadorService)
        {
            _indicadorService = indicadorService;
        }

        [HttpGet("/OrdemServico")]
        public List<Indicador> ObterIndicadoresOrdemServico([FromQuery] IndicadorParameters parameters)
        {
            return _indicadorService.ObterIndicadoresOrdemServico();
        }

        [HttpGet("/SLA")]
        public List<Indicador> ObterIndicadoresSLA([FromQuery] IndicadorParameters parameters)
        {
            throw new Exception("Nao Implementado");
        }

        [HttpGet("/Pendencia")]
        public List<Indicador> ObterIndicadoresPendencia([FromQuery] IndicadorParameters parameters)
        {
            throw new Exception("Nao Implementado");
        }

        [HttpGet("/Reincidencia")]
        public List<Indicador> ObterIndicadoresReincidencia([FromQuery] IndicadorParameters parameters)
        {
            throw new Exception("Nao Implementado");
        }
    }
}
