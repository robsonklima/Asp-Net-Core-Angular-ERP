using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TipoEquipamentoService : ITipoEquipamentoService
    {
        private readonly ITipoEquipamentoRepository _tipoEquipamentoRepo;

        public TipoEquipamentoService(ITipoEquipamentoRepository tipoEquipamentoRepo)
        {
            _tipoEquipamentoRepo = tipoEquipamentoRepo;
        }

        public ListViewModel ObterPorParametros(TipoEquipamentoParameters parameters)
        {
            var tiposEquipamento = _tipoEquipamentoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = tiposEquipamento,
                TotalCount = tiposEquipamento.TotalCount,
                CurrentPage = tiposEquipamento.CurrentPage,
                PageSize = tiposEquipamento.PageSize,
                TotalPages = tiposEquipamento.TotalPages,
                HasNext = tiposEquipamento.HasNext,
                HasPrevious = tiposEquipamento.HasPrevious
            };

            return lista;
        }

        public TipoEquipamento Criar(TipoEquipamento tipoEquipamento)
        {
            _tipoEquipamentoRepo.Criar(tipoEquipamento);
            return tipoEquipamento;
        }

        public void Deletar(int codigo)
        {
            _tipoEquipamentoRepo.Deletar(codigo);
        }

        public void Atualizar(TipoEquipamento tipoEquipamento)
        {
            _tipoEquipamentoRepo.Atualizar(tipoEquipamento);
        }

        public TipoEquipamento ObterPorCodigo(int codigo)
        {
            return _tipoEquipamentoRepo.ObterPorCodigo(codigo);
        }
    }
}
