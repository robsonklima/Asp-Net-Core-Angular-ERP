using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IEmailService
    {
        void Enviar(Email email);
    }
}