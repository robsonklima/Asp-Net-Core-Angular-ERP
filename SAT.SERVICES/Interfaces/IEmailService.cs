using System.Threading.Tasks;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IEmailService
    {
        void Enviar(Email email);
        Task<Office365Email> ObterEmailsAsync(string clientID);
        Task DeletarEmailAsync(string emailID, string clientID);
    }
}