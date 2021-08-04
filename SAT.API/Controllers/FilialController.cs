using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class FilialController : ControllerBase
    {
        private readonly IFilialRepository _filialInterface;

        public FilialController(IFilialRepository filialInterface)
        {
            _filialInterface = filialInterface;
        }

        [HttpGet]
        public FilialListViewModel Get([FromQuery] FilialParameters parameters)
        {
            var filiais = _filialInterface.ObterPorParametros(parameters);

            var filialListaViewModel = new FilialListViewModel
            {
                Filiais = filiais,
                TotalCount = filiais.TotalCount,
                CurrentPage = filiais.CurrentPage,
                PageSize = filiais.PageSize,
                TotalPages = filiais.TotalPages,
                HasNext = filiais.HasNext,
                HasPrevious = filiais.HasPrevious
            };

            return filialListaViewModel;
        }

        [HttpGet("{codFilial}")]
        public Filial Get(int codFilial)
        {
            return _filialInterface.ObterPorCodigo(codFilial);
        }

        [HttpPost]
        public void Post([FromBody] Filial filial)
        {
        }

        [HttpPut]
        public void Put([FromBody] Filial filial)
        {
        }

        [HttpDelete("{codFilial}")]
        public void Delete(int codFilial)
        {
        }
    }
}
