using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class GoogleGeolocationController : ControllerBase
    {
        [HttpGet]
        public async Task<GoogleGeolocation> Get([FromQuery] GoogleGeolocationParameters parameters)
        {
            var client = new HttpClient();
            GoogleGeolocation res = new GoogleGeolocation();

            var response = await client.GetAsync("https://maps.googleapis.com/maps/api/geocode/json?address=" +
                parameters.EnderecoCep + "&key=AIzaSyC4StJs8DtJZZIELzFgJckwrsvluzRo_WM");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var conteudo = await response.Content.ReadAsStringAsync();
                res = Newtonsoft.Json.JsonConvert.DeserializeObject<GoogleGeolocation>(conteudo);
            }

            return res;
        }
    }
}
