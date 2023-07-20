using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class AdendoService : IAdendoService
    {
        private readonly IAdendoRepository _AdendoRepo;

        public AdendoService(IAdendoRepository AdendoRepo)
        {
            _AdendoRepo = AdendoRepo;
        }

        public Adendo Atualizar(Adendo Adendo)
        {
            return _AdendoRepo.Atualizar(Adendo);
        }

        public Adendo Criar(Adendo Adendo)
        {
            _AdendoRepo.Criar(Adendo);

            return Adendo;
        }

        public Adendo Deletar(int codigo)
        {
            return _AdendoRepo.Deletar(codigo);
        }

        public Adendo ObterPorCodigo(int codigo)
        {
            return _AdendoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(AdendoParameters parameters)
        {
            var data = _AdendoRepo.ObterPorParametros(parameters);

            var model = new ListViewModel
            {
                Items = data,
                TotalCount = data.TotalCount,
                CurrentPage = data.CurrentPage,
                PageSize = data.PageSize,
                TotalPages = data.TotalPages,
                HasNext = data.HasNext,
                HasPrevious = data.HasPrevious
            };

            return model;
        }
    }
}
