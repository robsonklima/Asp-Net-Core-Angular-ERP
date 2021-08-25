using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadeFederativaController : ControllerBase
    {
        private readonly IUnidadeFederativaRepository _unidadeFederativaInterface;

        public UnidadeFederativaController(IUnidadeFederativaRepository unidadeFederativaInterface)
        {
            _unidadeFederativaInterface = unidadeFederativaInterface;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] UnidadeFederativaParameters parameteres)
        {
            var ufs = _unidadeFederativaInterface.ObterPorParametros(parameteres);

            var lista = new ListViewModel
            {
                Items = ufs,
                TotalCount = ufs.TotalCount,
                CurrentPage = ufs.CurrentPage,
                PageSize = ufs.PageSize,
                TotalPages = ufs.TotalPages,
                HasNext = ufs.HasNext,
                HasPrevious = ufs.HasPrevious
            };

            return lista;
        }

        [HttpGet("{codUF}")]
        public UnidadeFederativa Get(int codUF)
        {
            return _unidadeFederativaInterface.ObterPorCodigo(codUF);
        }

        [HttpPost]
        public void Post([FromBody] UnidadeFederativa uf)
        {
            _unidadeFederativaInterface.Criar(uf);
        }

        [HttpPut]
        public void Put([FromBody] UnidadeFederativa uf)
        {
            _unidadeFederativaInterface.Atualizar(uf);
        }

        [HttpDelete("{codUF}")]
        public void Delete(int codUF)
        {
            _unidadeFederativaInterface.Deletar(codUF);
        }
    }
}
