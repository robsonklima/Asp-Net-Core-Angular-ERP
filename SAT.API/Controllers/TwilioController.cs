using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class TwilioController : ControllerBase
    {
        private readonly ITwilioService _twilioService;

        public TwilioController(ITwilioService twilioService)
        {
            _twilioService = twilioService;
        }

        [HttpGet]
        public void Get()
        {
            _twilioService.Enviar();
        }
    }
}
