using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DefeitoPOSService : IDefeitoPOSService
    {
        private readonly IDefeitoPOSRepository _motivoRepo;

        public DefeitoPOSService(IDefeitoPOSRepository motivoRepo)
        {
            _motivoRepo = motivoRepo;
        }

        public ListViewModel ObterPorParametros(DefeitoPOSParameters parameters)
        {
            var regioes = _motivoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = regioes,
                TotalCount = regioes.TotalCount,
                CurrentPage = regioes.CurrentPage,
                PageSize = regioes.PageSize,
                TotalPages = regioes.TotalPages,
                HasNext = regioes.HasNext,
                HasPrevious = regioes.HasPrevious
            };

            return lista;
        }

        public DefeitoPOS Criar(DefeitoPOS d)
        {
            _motivoRepo.Criar(d);

            return d;
        }

        public DefeitoPOS Deletar(int codigo)
        {
            return _motivoRepo.Deletar(codigo);
        }

        public DefeitoPOS Atualizar(DefeitoPOS d)
        {
            return _motivoRepo.Atualizar(d);
        }

        public DefeitoPOS ObterPorCodigo(int codigo)
        {
            return _motivoRepo.ObterPorCodigo(codigo);
        }
    }
}
