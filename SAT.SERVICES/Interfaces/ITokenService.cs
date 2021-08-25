using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(string key, string issuer, Usuario usuario);
        bool IsTokenValido(string key, string issuer, string token);
    }
}
