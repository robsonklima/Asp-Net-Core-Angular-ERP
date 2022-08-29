using System.Threading.Tasks;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IEmailService
    {
        void Enviar(Email email);
        Task<string> ObterTokenAsync();
        Task<Office365Email> ObterEmailsAsync(string token, string clientID);
        Task DeletarEmailAsync(string token, string emailID, string clientID);
    }
}