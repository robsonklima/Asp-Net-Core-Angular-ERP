using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using System.Collections.Generic;
using SAT.INFRA.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TraducaoController : ControllerBase
    {
        private readonly ITraducaoRepository _traducaoInterface;

        public TraducaoController(ITraducaoRepository traducaoInterface)
        {
            _traducaoInterface = traducaoInterface;
        }

        [HttpGet]
        public IEnumerable<Traducao> Get()
        {
            IEnumerable<Traducao> traducoes;

            traducoes = _traducaoInterface.ObterPorParametros(registros: 100);

            return traducoes;
        }
    }
}
