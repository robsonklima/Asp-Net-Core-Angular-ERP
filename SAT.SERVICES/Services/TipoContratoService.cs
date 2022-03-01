using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TipoContratoService : ITipoContratoService
    {
        private readonly ITipoContratoRepository _tipoContratoRepo;

        public TipoContratoService(ITipoContratoRepository tipoContratoRepo)
        {
            _tipoContratoRepo = tipoContratoRepo;
        }

        public ListViewModel ObterPorParametros(TipoContratoParameters parameters)
        {
            var tipoContrato = _tipoContratoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = tipoContrato,
                TotalCount = tipoContrato.TotalCount,
                CurrentPage = tipoContrato.CurrentPage,
                PageSize = tipoContrato.PageSize,
                TotalPages = tipoContrato.TotalPages,
                HasNext = tipoContrato.HasNext,
                HasPrevious = tipoContrato.HasPrevious
            };

            return lista;
        }

        public TipoContrato Criar(TipoContrato tipoContrato)
        {
            _tipoContratoRepo.Criar(tipoContrato);
            return tipoContrato;
        }

        public void Deletar(int codigo)
        {
            _tipoContratoRepo.Deletar(codigo);
        }

        public void Atualizar(TipoContrato tipoContrato)
        {
            _tipoContratoRepo.Atualizar(tipoContrato);
        }

        public TipoContrato ObterPorCodigo(int codigo)
        {
            return _tipoContratoRepo.ObterPorCodigo(codigo);
        }
    }
}
