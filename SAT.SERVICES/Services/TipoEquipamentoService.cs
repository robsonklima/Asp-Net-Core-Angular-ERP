using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TipoEquipamentoService : ITipoEquipamentoService
    {
        private readonly ITipoEquipamentoRepository _tipoEquipamentoRepo;
        private readonly ISequenciaRepository _seqRepo;

        public TipoEquipamentoService(ITipoEquipamentoRepository tipoEquipamentoRepo, ISequenciaRepository seqRepo)
        {
            _tipoEquipamentoRepo = tipoEquipamentoRepo;
            _seqRepo = seqRepo;
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
            tipoEquipamento.CodTipoEquip = _seqRepo.ObterContador(Constants.TABELA_TIPO_EQUIPAMENTO);
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
