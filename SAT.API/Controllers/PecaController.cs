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
    public class PecaController : ControllerBase
    {
        private readonly IPecaRepository _pecaInterface;

        public PecaController(IPecaRepository pecaInterface)
        {
            _pecaInterface = pecaInterface;
        }

        [HttpGet]
        public PecaListViewModel Get([FromQuery] PecaParameters parameters)
        {
            var pecas = _pecaInterface.ObterPorParametros(parameters);

            var lista = new PecaListViewModel
            {
                Pecas = pecas,
                TotalCount = pecas.TotalCount,
                CurrentPage = pecas.CurrentPage,
                PageSize = pecas.PageSize,
                TotalPages = pecas.TotalPages,
                HasNext = pecas.HasNext,
                HasPrevious = pecas.HasPrevious
            };

            return lista;
        }

        [HttpGet("{codPeca}")]
        public Peca Get(int codPeca)
        {
            return _pecaInterface.ObterPorCodigo(codPeca);
        }

        [HttpPost]
        public void Post([FromBody] Peca peca)
        {
            _pecaInterface.Criar(peca);
        }

        [HttpPut]
        public void Put([FromBody] Peca peca)
        {
            _pecaInterface.Atualizar(peca);
        }

        [HttpDelete("{codPeca}")]
        public void Delete(int codPeca)
        {
            _pecaInterface.Deletar(codPeca);
        }
    }
}
