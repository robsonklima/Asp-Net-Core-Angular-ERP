using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class VersaoService : IVersaoService
    {
        private readonly IVersaoRepository _versaoRepo;

        public VersaoService(
            IVersaoRepository VersaoRepo
        )
        {
            _versaoRepo = VersaoRepo;
        }

        public ListViewModel ObterPorParametros(VersaoParameters parameters)
        {
            var Versaos = _versaoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = Versaos,
                TotalCount = Versaos.TotalCount,
                CurrentPage = Versaos.CurrentPage,
                PageSize = Versaos.PageSize,
                TotalPages = Versaos.TotalPages,
                HasNext = Versaos.HasNext,
                HasPrevious = Versaos.HasPrevious
            };

            return lista;
        }

        public Versao Criar(Versao Versao)
        {
            _versaoRepo.Criar(Versao);
            return Versao;
        }

        public void Deletar(int codigo)
        {
            _versaoRepo.Deletar(codigo);
        }

        public void Atualizar(Versao Versao)
        {
            _versaoRepo.Atualizar(Versao);
        }

        public Versao ObterPorCodigo(int codigo)
        {
            return _versaoRepo.ObterPorCodigo(codigo);
        }
    }
}
