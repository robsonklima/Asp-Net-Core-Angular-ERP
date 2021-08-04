using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;


namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TecnicoOrdemServicoController : ControllerBase
    {
        private readonly ITecnicoOrdemServicoRepository _tecnicoOrdemServicoInterface;

        public TecnicoOrdemServicoController(
            ITecnicoOrdemServicoRepository tecnicoOrdemServicoInterface
        )
        {
            _tecnicoOrdemServicoInterface = tecnicoOrdemServicoInterface;
        }

        [HttpGet]
        public TecnicoListViewModel Get([FromQuery] TecnicoOrdemServicoParameters parameters)
        {
            var tecnicos = _tecnicoOrdemServicoInterface.ObterPorParametros(parameters);

            var lista = new TecnicoListViewModel
            {
                Tecnicos = tecnicos,
                TotalCount = tecnicos.TotalCount,
                CurrentPage = tecnicos.CurrentPage,
                PageSize = tecnicos.PageSize,
                TotalPages = tecnicos.TotalPages,
                HasNext = tecnicos.HasNext,
                HasPrevious = tecnicos.HasPrevious
            };

            return lista;
        }
    }
}
