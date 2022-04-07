using SAT.SERVICES.Interfaces;
using Vonage;
using Vonage.Request;

namespace SAT.SERVICES.Services
{
    public class SmsService : ISmsService
    {
        public void Enviar()
        {
            var credentials = Credentials.FromApiKeyAndSecret(
                "eab57cf8",
                "NX7ZdN7nNDrxoNyC"
                );

            var VonageClient = new VonageClient(credentials);

            var response = VonageClient.SmsClient.SendAnSms(new Vonage.Messaging.SendSmsRequest()
            {
                To = "5551985168784",
                From = "SAT",
                Text = "A text message sent using the Vonage SMS API"
            });
        }
    }
}
