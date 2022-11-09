﻿using Microsoft.AspNetCore.Authorization;
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
    public class TicketStatusController : ControllerBase
    {
        private readonly ITicketStatusService _ticketStatusService;

        public TicketStatusController(
            ITicketStatusService ticketStatusService
        )
        {
            _ticketStatusService = ticketStatusService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] TicketStatusParameters parameters)
        {
            return _ticketStatusService.ObterPorParametros(parameters);
        }

        [HttpGet("{codStatus}")]
        public TicketStatus Get(int codStatus)
        {
            return _ticketStatusService.ObterPorCodigo(codStatus);
        }

        [HttpPut]
        public void Put([FromBody] TicketStatus ticketStatus)
        {
            _ticketStatusService.Atualizar(ticketStatus);
        }
    }
}
