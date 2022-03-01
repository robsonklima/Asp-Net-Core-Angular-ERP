using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TipoIndiceReajusteService : ITipoIndiceReajusteService
    {
        private readonly ITipoIndiceReajusteRepository _tipoIndiceReajusteRepo;

        public TipoIndiceReajusteService(ITipoIndiceReajusteRepository tipoIndiceReajusteRepo)
        {
            _tipoIndiceReajusteRepo = tipoIndiceReajusteRepo;
        }

        public ListViewModel ObterPorParametros(TipoIndiceReajusteParameters parameters)
        {
            var tipoIndiceReajuste = _tipoIndiceReajusteRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = tipoIndiceReajuste,
                TotalCount = tipoIndiceReajuste.TotalCount,
                CurrentPage = tipoIndiceReajuste.CurrentPage,
                PageSize = tipoIndiceReajuste.PageSize,
                TotalPages = tipoIndiceReajuste.TotalPages,
                HasNext = tipoIndiceReajuste.HasNext,
                HasPrevious = tipoIndiceReajuste.HasPrevious
            };

            return lista;
        }

        public TipoIndiceReajuste Criar(TipoIndiceReajuste tipoIndiceReajuste)
        {
            _tipoIndiceReajusteRepo.Criar(tipoIndiceReajuste);
            return tipoIndiceReajuste;
        }

        public void Deletar(int codigo)
        {
            _tipoIndiceReajusteRepo.Deletar(codigo);
        }

        public void Atualizar(TipoIndiceReajuste tipoIndiceReajuste)
        {
            _tipoIndiceReajusteRepo.Atualizar(tipoIndiceReajuste);
        }

        public TipoIndiceReajuste ObterPorCodigo(int codigo)
        {
            return _tipoIndiceReajusteRepo.ObterPorCodigo(codigo);
        }
    }
}
