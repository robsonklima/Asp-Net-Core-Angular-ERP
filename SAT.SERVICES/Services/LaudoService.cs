using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class LaudoService : ILaudoService
    {
        private readonly ILaudoRepository _laudoRepo;

        public LaudoService(ILaudoRepository laudoRepo)
        {
            _laudoRepo = laudoRepo;
        }
        public Laudo ObterPorCodigo(int codigo) =>
            this._laudoRepo.ObterPorCodigo(codigo);
    }
}