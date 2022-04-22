using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;
using Vonage;
using Vonage.Messaging;
using Vonage.Request;
using System.Text.RegularExpressions;

namespace SAT.SERVICES.Services
{
    public class SmsService : ISmsService
    {
        public void Enviar(Sms sms)
        {
            var credentials = Credentials.FromApiKeyAndSecret(Constants.VONAGE_KEY, Constants.VONAGE_SECRET);
            var VonageClient = new VonageClient(credentials);

            var response = VonageClient.SmsClient.SendAnSms(new SendSmsRequest()
            {
                To = FormatarFone(sms.To),
                From = sms.From,
                Text = sms.Text
            });
        }

        private string FormatarFone(string fone) {
            fone = Regex.Replace(fone, "[^0-9]", "");

            if (!fone.Contains("+55")) 
                fone = "+55" + fone;

            return fone;
        }
    }
}
