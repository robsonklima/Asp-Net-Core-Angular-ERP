using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;


namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly IPaisRepository _paisInterface;

        public PaisController(IPaisRepository paisInterface)
        {
            _paisInterface = paisInterface;
        }

        [HttpGet]
        public PaisListViewModel Get([FromQuery] PaisParameters parameters)
        {
            var paises = _paisInterface.ObterPorParametros(parameters);

            var paisListViewModel = new PaisListViewModel
            {
                Paises = paises,
                TotalCount = paises.TotalCount,
                CurrentPage = paises.CurrentPage,
                PageSize = paises.PageSize,
                TotalPages = paises.TotalPages,
                HasNext = paises.HasNext,
                HasPrevious = paises.HasPrevious
            };

            return paisListViewModel;
        }

        [HttpGet("{codPais}")]
        public Pais Get(int codPais)
        {
            return _paisInterface.ObterPorCodigo(codPais);
        }

        [HttpPost]
        public void Post([FromBody] Pais pais)
        {
            _paisInterface.Criar(pais);
        }

        [HttpPut]
        public void Put([FromBody] Pais pais)
        {
            _paisInterface.Atualizar(pais);
        }

        [HttpDelete("{codPais}")]
        public void Delete(int codPais)
        {
            _paisInterface.Deletar(codPais);
        }
    }
}
