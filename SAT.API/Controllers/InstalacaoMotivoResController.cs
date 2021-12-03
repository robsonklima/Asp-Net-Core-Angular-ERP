using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class InstalacaoMotivoResController : ControllerBase
    {
        private readonly IInstalacaoMotivoResService _instalacaoMotivoResService;

        public InstalacaoMotivoResController(
            IInstalacaoMotivoResService instalacaoMotivoResService
        )
        {
            _instalacaoMotivoResService = instalacaoMotivoResService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] InstalacaoMotivoResParameters parameters)
        {
            return _instalacaoMotivoResService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodInstalMotivoRes}")]
        public InstalacaoMotivoRes Get(int CodInstalMotivoRes)
        {
            return _instalacaoMotivoResService.ObterPorCodigo(CodInstalMotivoRes);
        }

        [HttpPost]
        public InstalacaoMotivoRes Post([FromBody] InstalacaoMotivoRes instalacaoMotivoRes)
        {
            return _instalacaoMotivoResService.Criar(instalacaoMotivoRes);
        }

        [HttpPut]
        public void Put([FromBody] InstalacaoMotivoRes instalacaoMotivoRes)
        {
            _instalacaoMotivoResService.Atualizar(instalacaoMotivoRes);
        }

        [HttpDelete("{CodInstalMotivoRes}")]
        public void Delete(int CodInstalMotivoRes)
        {
            _instalacaoMotivoResService.Deletar(CodInstalMotivoRes);
        }
    }
}
