using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class AuditoriaFotoService : IAuditoriaFotoService
    {
        private readonly IAuditoriaFotoRepository _auditoriaFotoRepo;
        public AuditoriaFotoService( IAuditoriaFotoRepository auditoriaFotoRepo ) 
        {
            _auditoriaFotoRepo = auditoriaFotoRepo;
        }

        public void Atualizar(AuditoriaFoto auditoriaFoto)
        {
            _auditoriaFotoRepo.Atualizar(auditoriaFoto);
        }

        public void Criar(AuditoriaFoto auditoriaFoto)
        {
            _auditoriaFotoRepo.Criar(auditoriaFoto);
        }

        public void Deletar(int codigoAuditoriaFoto)
        {
            _auditoriaFotoRepo.Deletar(codigoAuditoriaFoto);
        }

        public AuditoriaFoto ObterPorCodigo(int codAuditoriaFoto)
        {
            return _auditoriaFotoRepo.ObterPorCodigo(codAuditoriaFoto);
        }

        public ListViewModel ObterPorParametros(AuditoriaFotoParameters parameters)
        {
            var auditoriasFoto = _auditoriaFotoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = auditoriasFoto,
                TotalCount = auditoriasFoto.TotalCount,
                CurrentPage = auditoriasFoto.CurrentPage,
                PageSize = auditoriasFoto.PageSize,
                TotalPages = auditoriasFoto.TotalPages,
                HasNext = auditoriasFoto.HasNext,
                HasPrevious = auditoriasFoto.HasPrevious
            };

            return lista;
        }
    }
}
