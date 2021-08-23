using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using System.Collections.Generic;

namespace SAT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoEquipamentoController : ControllerBase
    {
        public IContratoEquipamentoRepository _contratoEquipamentoInterface { get; }

        public ContratoEquipamentoController(
            IContratoEquipamentoRepository contratoEquipamentoInterface
        )
        {
            _contratoEquipamentoInterface = contratoEquipamentoInterface;
        }

        [HttpGet]
        public ContratoEquipamentoListViewModel Get([FromQuery] ContratoEquipamentoParameters parameters)
        {
            var contratosEquipamento = _contratoEquipamentoInterface.ObterPorParametros(parameters);

            var lista = new ContratoEquipamentoListViewModel
            {
                ContratosEquipamento= contratosEquipamento,
                TotalCount = contratosEquipamento.TotalCount,
                CurrentPage = contratosEquipamento.CurrentPage,
                PageSize = contratosEquipamento.PageSize,
                TotalPages = contratosEquipamento.TotalPages,
                HasNext = contratosEquipamento.HasNext,
                HasPrevious = contratosEquipamento.HasPrevious
            };

            return lista;
        }
    }
}
