using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using System.Collections.Generic;


namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class TecnicoController : ControllerBase
    {
        private readonly ITecnicoRepository _tecnicoInterface;

        public TecnicoController(ITecnicoRepository tecnicoInterface)
        {
            _tecnicoInterface = tecnicoInterface;
        }

        [HttpGet]
        public TecnicoListViewModel Get([FromQuery] TecnicoParameters parameters)
        {
            var tecnicos = _tecnicoInterface.ObterPorParametros(parameters);

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

        [HttpGet("{codTecnico}")]
        public Tecnico Get(int codTecnico)
        {
            return _tecnicoInterface.ObterPorCodigo(codTecnico);
        }

        [HttpPost]
        public void Post([FromBody] Tecnico tecnico)
        {
            _tecnicoInterface.Criar(tecnico);
        }

        [HttpPut]
        public void Put([FromBody] Tecnico tecnico)
        {
            _tecnicoInterface.Atualizar(tecnico);
        }

        [HttpDelete("{codTecnico}")]
        public void Delete(int codTecnico)
        {
            _tecnicoInterface.Deletar(codTecnico);
        }
    }
}
