using System;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class EquipamentoContratoService : IEquipamentoContratoService
    {
        private readonly IEquipamentoContratoRepository _equipamentoContratoRepo;
        private readonly IEquipamentoRepository _equipamentoRepo;
        private readonly IOrdemServicoRepository _ordemServicoRepo;
        private readonly ISequenciaRepository _seqRepo;

        public EquipamentoContratoService(
            IEquipamentoContratoRepository equipamentoContratoRepo,
            IEquipamentoRepository equipamentoRepo,
            IOrdemServicoRepository ordemServicoRepo,
            ISequenciaRepository seqRepo
        )
        {
            _equipamentoContratoRepo = equipamentoContratoRepo;
            _equipamentoRepo = equipamentoRepo;
            _ordemServicoRepo = ordemServicoRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(EquipamentoContratoParameters parameters)
        {
            var equipamentoContratos = _equipamentoContratoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = equipamentoContratos,
                TotalCount = equipamentoContratos.TotalCount,
                CurrentPage = equipamentoContratos.CurrentPage,
                PageSize = equipamentoContratos.PageSize,
                TotalPages = equipamentoContratos.TotalPages,
                HasNext = equipamentoContratos.HasNext,
                HasPrevious = equipamentoContratos.HasPrevious
            };

            return lista;
        }

        public EquipamentoContrato Criar(EquipamentoContrato equipamentoContrato)
        {
            equipamentoContrato.CodEquipContrato = this._seqRepo.ObterContador("EquipamentoContrato");
            var equip = _equipamentoRepo.ObterPorCodigo(equipamentoContrato.CodEquip.Value);
            equipamentoContrato.CodTipoEquip = equip.CodTipoEquip;
            equipamentoContrato.CodGrupoEquip = equip.CodGrupoEquip;

            _equipamentoContratoRepo.Criar(equipamentoContrato);
            return equipamentoContrato;
        }

        public void Deletar(int codigo)
        {
            _equipamentoContratoRepo.Deletar(codigo);
        }

        public void Atualizar(EquipamentoContrato equipamentoContrato)
        {
            var equip = _equipamentoRepo.ObterPorCodigo(equipamentoContrato.CodEquip.Value);
            equipamentoContrato.CodTipoEquip = equip.CodTipoEquip;
            equipamentoContrato.CodGrupoEquip = equip.CodGrupoEquip;
            _equipamentoContratoRepo.Atualizar(equipamentoContrato);
        }

        public EquipamentoContrato ObterPorCodigo(int codigo)
        {
            return _equipamentoContratoRepo.ObterPorCodigo(codigo);
        }

        public MtbfEquipamento CalcularMTBF(int codEquipContrato, DateTime? dataInicio, DateTime dataFim)
        {
            if (!dataInicio.HasValue)
                dataInicio = DateTime.Now.AddDays(-90);

            PagedList<OrdemServico> _ordensServico = _ordemServicoRepo.ObterPorParametros(new OrdemServicoParameters {
                CodEquipContrato = codEquipContrato,
                DataHoraInicioInicio = dataInicio,
                DataHoraInicioFim = dataFim
            });

            var qtdOS = _ordensServico.Count;

            if (qtdOS == 0) 
                qtdOS = 1;

            var totalDias = (dataFim - dataInicio).Value.Days;

            MtbfEquipamento mtbf = new MtbfEquipamento {
                Inicio = dataInicio,
                Fim = dataFim,
                Resultado = totalDias / qtdOS,
                QtdOS = qtdOS
            };

            return mtbf;
        }
    }
}
