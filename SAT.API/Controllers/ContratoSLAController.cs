using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using System.Collections.Generic;

namespace SAT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoSLAController : ControllerBase
    {
        public IContratoSLARepository _contratoSLAInterface { get; }

        public ContratoSLAController(
            IContratoSLARepository contratoSLAInterface
        )
        {
            _contratoSLAInterface = contratoSLAInterface;
        }

        [HttpGet]
        public ContratoSLAListViewModel Get([FromQuery] ContratoSLAParameters parameters)
        {
            var contratosSLA = _contratoSLAInterface.ObterPorParametros(parameters);

            var lista = new ContratoSLAListViewModel
            {
                ContratosSLA = contratosSLA,
                TotalCount = contratosSLA.TotalCount,
                CurrentPage = contratosSLA.CurrentPage,
                PageSize = contratosSLA.PageSize,
                TotalPages = contratosSLA.TotalPages,
                HasNext = contratosSLA.HasNext,
                HasPrevious = contratosSLA.HasPrevious
            };

            return lista;
        }
    }
}
