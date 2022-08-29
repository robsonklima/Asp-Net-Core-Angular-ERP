using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Threading.Tasks;

namespace SAT.API.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class Office365Controller : ControllerBase
    {
        private readonly IOffice365Service _office365Service;

        public Office365Controller(IOffice365Service office365Service)
        {
            _office365Service = office365Service;
        }

        [HttpGet("{token}/{clientID}")]
        public Task<Office365Email> Get(string token, string clientID)
        {
            return _office365Service.ObterEmailsAsync(token, clientID);
        }

        [HttpDelete("{token}/{emailID}/{clientID}")]
        public void Delete(string token, string emailID, string clientID)
        {
            _office365Service.DeletarEmailAsync(token, emailID, clientID);
        }
    }
}
