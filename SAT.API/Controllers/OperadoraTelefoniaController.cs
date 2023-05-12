using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class OperadoraTelefoniaController : ControllerBase
    {
        private IOperadoraTelefoniaService _operadoraTelefoniaService;

        public OperadoraTelefoniaController(
            IOperadoraTelefoniaService OperadoraTelefoniaService
        )
        {
            _operadoraTelefoniaService = OperadoraTelefoniaService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] OperadoraTelefoniaParameters parameters)
        {
            return _operadoraTelefoniaService.ObterPorParametros(parameters);
        }

        [HttpGet("{codOperadoraTelefonia}")]
        public OperadoraTelefonia Get(int codOperadoraTelefonia)
        {
            return _operadoraTelefoniaService.ObterPorCodigo(codOperadoraTelefonia);
        }

        [HttpPost]
        public OperadoraTelefonia Post([FromBody] OperadoraTelefonia op)
        {
            return _operadoraTelefoniaService.Criar(op);
        }

        [HttpPut]
        public OperadoraTelefonia Put([FromBody] OperadoraTelefonia op)
        {
            return _operadoraTelefoniaService.Atualizar(op);
        }

        [HttpDelete("{codOperadoraTelefonia}")]
        public OperadoraTelefonia Delete(int codOperadoraTelefonia)
        {
            return _operadoraTelefoniaService.Deletar(codOperadoraTelefonia);
        }
    }
}
