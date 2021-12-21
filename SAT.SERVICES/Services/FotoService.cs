using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class FotoService : IFotoService
    {
        private readonly IFotoRepository _fotoRepo;

        public FotoService(IFotoRepository fotoRepo)
        {
            _fotoRepo = fotoRepo;
        }

        public Foto Criar(Foto foto)
        {
            _fotoRepo.Criar(foto);
            return foto;
        }

        public void Deletar(int codigo)
        {
            _fotoRepo.Deletar(codigo);
        }

        public Foto ObterPorCodigo(int codigo)
        {
            return _fotoRepo.ObterPorCodigo(codigo);
        }
    }
}
