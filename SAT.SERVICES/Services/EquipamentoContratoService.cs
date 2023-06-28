using System;
using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
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
        private readonly IContratoServicoRepository _contratoServicoRepo;
        private readonly ISequenciaRepository _seqRepo;
        private readonly IContratoEquipamentoService _contratoEquipamentoService;

        public EquipamentoContratoService(
            IEquipamentoContratoRepository equipamentoContratoRepo,
            IEquipamentoRepository equipamentoRepo,
            IOrdemServicoRepository ordemServicoRepo,
            IContratoServicoRepository contratoServicoRepo,
            ISequenciaRepository seqRepo,
            IContratoEquipamentoService contratoEquipamentoService
        )
        {
            _equipamentoContratoRepo = equipamentoContratoRepo;
            _equipamentoRepo = equipamentoRepo;
            _ordemServicoRepo = ordemServicoRepo;
            _contratoServicoRepo = contratoServicoRepo;
            _seqRepo = seqRepo;
            _contratoEquipamentoService = contratoEquipamentoService;
        }

        public ListViewModel ObterPorParametros(EquipamentoContratoParameters parameters)
        {
            var equipamentoContratos = _equipamentoContratoRepo.ObterPorParametros(parameters);

            equipamentoContratos.ForEach(e => {
                e.ValorSoftwareEmbarcado = CalcularTotalServico(e, Constants.TIPO_SERVICO_SOFTWARE_EMBARCADO);
                e.ValorMonitoramentoRemoto = CalcularTotalServico(e, Constants.TIPO_SERVICO_MONITORAMENTO_REMOTO);
            });

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
            EquipamentoContrato equip = _equipamentoContratoRepo.ObterPorCodigo(codigo);
            equip.ValorSoftwareEmbarcado = CalcularTotalServico(equip, Constants.TIPO_SERVICO_SOFTWARE_EMBARCADO);
            equip.ValorMonitoramentoRemoto = CalcularTotalServico(equip, Constants.TIPO_SERVICO_MONITORAMENTO_REMOTO);
            return equip;
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

        private decimal CalcularTotalServico(EquipamentoContrato equip, int codServico) {
            var servicos = _contratoServicoRepo.ObterPorParametros(new ContratoServicoParameters {
                CodEquip = equip.CodEquip,
                CodContrato = equip.CodContrato,
                CodSLA = equip.CodSLA,
                CodServico = codServico
            });

            return servicos.Sum(s => s.Valor);
        }
    }
}
