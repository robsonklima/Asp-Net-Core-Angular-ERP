﻿using Microsoft.AspNetCore.Mvc;
using SAT.MODELS;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;
using System.Collections.Generic;

namespace SAT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndicadorController : ControllerBase
    {
        private readonly IIndicadorService _indicadorService;

        public IndicadorController(
            IIndicadorService indicadorService
        )
        {
            _indicadorService = indicadorService;
        }

        [HttpGet]
        public List<Indicador> Get([FromQuery] IndicadorParameters parameters)
        {
            return _indicadorService.ObterIndicadoresClientes();
        }
    }
}
