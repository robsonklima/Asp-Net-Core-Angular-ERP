using System.Linq;
using Newtonsoft.Json;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrdemServicoService : IOrdemServicoService
    {
        private readonly IOrdemServicoRepository _ordemServicoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;
        private readonly IOrdemServicoAlertaService _ordemServicoAlertaService;
        public OrdemServicoService(IOrdemServicoRepository ordemServicoRepo, ISequenciaRepository sequenciaRepo, IOrdemServicoAlertaService ordemServicoAlertaService)
        {
            _ordemServicoRepo = ordemServicoRepo;
            _sequenciaRepo = sequenciaRepo;
            _ordemServicoAlertaService = ordemServicoAlertaService;
        }

        public OrdemServico Atualizar(OrdemServico ordemServico)
        {
            LoggerService.LogInfo("Chamado Atualizado pelo SAT 2.0: " + JsonConvert.SerializeObject(ordemServico));

            _ordemServicoRepo.Atualizar(ordemServico);

            return ordemServico;
        }

        public OrdemServico Criar(OrdemServico ordemServico)
        {
            ordemServico.CodOS = _sequenciaRepo.ObterContador("OS");

            _ordemServicoRepo.Criar(ordemServico);

            return ordemServico;
        }

        public void Deletar(int codOS)
        {
            _ordemServicoRepo.Deletar(codOS);
        }

        public OrdemServico ObterPorCodigo(int codigo)
        {
            OrdemServico os = _ordemServicoRepo.ObterPorCodigo(codigo);

            if (os != null)
            {
                os.Alertas = _ordemServicoAlertaService.ObterAlertas(os);
                os.IndNumRATObrigatorio = VerificarNumeroRATObrigatorio(os);
            }

            return os;
        }

        public ListViewModel ObterPorParametros(OrdemServicoParameters parameters)
        {
            var ordensServico = _ordemServicoRepo.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = ordensServico,
                TotalCount = ordensServico.TotalCount,
                CurrentPage = ordensServico.CurrentPage,
                PageSize = ordensServico.PageSize,
                TotalPages = ordensServico.TotalPages,
                HasNext = ordensServico.HasNext,
                HasPrevious = ordensServico.HasPrevious
            };
        }

        private bool VerificarNumeroRATObrigatorio(OrdemServico os)
        {
            if (os.CodTipoIntervencao == (int)TipoIntervencaoEnum.INSTALACAO)
            {
                return true;
            }

            if (
                (
                    os.CodTipoIntervencao == (int)TipoIntervencaoEnum.CORRETIVA
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.PREVENTIVA
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_APROVADO
                )
                &&
                (
                    os.CodCliente == (int)ClienteEnum.ZAFFARI
                    || os.CodCliente == (int)ClienteEnum.BANRISUL
                    || os.CodCliente == (int)ClienteEnum.CEF
                    || os.CodCliente == (int)ClienteEnum.SICREDI
                    || os.CodCliente == (int)ClienteEnum.CORREIOS
                    || os.CodCliente == (int)ClienteEnum.BB
                    || os.CodCliente == (int)ClienteEnum.BANPARA
                    || os.CodCliente == (int)ClienteEnum.BRB
                    || os.CodCliente == (int)ClienteEnum.BRB_OUTSOURCING
                    || os.CodCliente == (int)ClienteEnum.BANCO_DA_AMAZONIA
                    || os.CodCliente == (int)ClienteEnum.BNB
                    || os.CodCliente == (int)ClienteEnum.BANESTES
                )
            )
            {
                return true;
            }

            if (os.CodCliente == (int)ClienteEnum.BB
                &&
                (os.CodTipoIntervencao == (int)TipoIntervencaoEnum.CORRETIVA
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.PREVENTIVA
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORCAMENTO
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_APROVADO
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_REPROVADO
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_PEND_APROVACAO_CLIENTE
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_PEND_FILIAL_DETALHAR_MOTIVO
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.INSPECAO_TECNICA)
                &&
                os.CodContrato != (int)ContratoEnum.BB_TECNO_0125_2017)
            {
                return true;
            }

            if (os.CodCliente == (int)ClienteEnum.BB
                &&
                (os.CodTipoIntervencao == (int)TipoIntervencaoEnum.CORRETIVA
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.PREVENTIVA
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORCAMENTO
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_APROVADO
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_REPROVADO
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_PEND_APROVACAO_CLIENTE
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_PEND_FILIAL_DETALHAR_MOTIVO
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.INSPECAO_TECNICA
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.MANUTENCAO_GERENCIAL
                    || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.COFRE)
                &&
                os.CodContrato == (int)ContratoEnum.BB_TECNO_0125_2017)
            {
                return true;
            }

            return false;
        }        
    }
}