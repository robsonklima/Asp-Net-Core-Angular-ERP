using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class MensagemTecnicoController : ControllerBase
    {
        private readonly IMensagemTecnicoService _mensagemTecnicoService;

        public MensagemTecnicoController(IMensagemTecnicoService mensagemTecnicoService)
        {
            _mensagemTecnicoService = mensagemTecnicoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] MensagemTecnicoParameters parameters) =>
            _mensagemTecnicoService.ObterPorParametros(parameters);

        [HttpGet("{CodMensagemTecnico}")]
        public MensagemTecnico Get(int codMensagemTecnico) =>
            _mensagemTecnicoService.ObterPorCodigo(codMensagemTecnico);

        [HttpPost]
        public void Post([FromBody] MensagemTecnico msg) =>
            _mensagemTecnicoService.Criar(msg);

        [HttpPut]
        public MensagemTecnico Put([FromBody] MensagemTecnico msg) =>
            _mensagemTecnicoService.Atualizar(msg);

        [HttpDelete("{CodMensagemTecnico}")]
        public void Delete(int codMensagemTecnico) =>
            _mensagemTecnicoService.Deletar(codMensagemTecnico);
    }
}