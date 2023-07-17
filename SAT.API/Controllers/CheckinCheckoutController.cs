using System.Security.Claims;
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
    public class CheckinCheckoutController : ControllerBase
    {
        private readonly ICheckinCheckoutService _checkinCheckoutService;

        public CheckinCheckoutController(ICheckinCheckoutService checkinCheckoutService)
        {
            _checkinCheckoutService = checkinCheckoutService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] CheckinCheckoutParameters parameters)
        {
            return this._checkinCheckoutService.ObterPorParametros(parameters);
        }
    }
}
