using SAT.SERVICES.Interfaces;
using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace SAT.SERVICES.Services
{
    public class TwilioService : ITwilioService
    {
        public void Enviar()
        {
            string accountSid = "AC98f71aed54c6c33055fd5cbc5f2840a7";
            string authToken = "47460ef3dfd99c9785b13873e7c668dd";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Hello there!",
                from: new Twilio.Types.PhoneNumber("+17578599405"),
                to: new Twilio.Types.PhoneNumber("+5551985168784")
            );

            Console.WriteLine(message.Sid);
        }
    }
}
