using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using NLog.Fluent;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class IntegracaoBBService : IIntegracaoBBService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private IOrdemServicoService _ordemServicoService;
        private ILocalAtendimentoService _localAtendimentoService;
        private IEquipamentoContratoService _equipamentoContratoService;

        public IntegracaoBBService(
            IOrdemServicoService ordemServicoService,
            ILocalAtendimentoService localAtendimentoService,
            IEquipamentoContratoService equipamentoContratoService
        )
        {
            _ordemServicoService = ordemServicoService;
            _localAtendimentoService = localAtendimentoService;
            _equipamentoContratoService = equipamentoContratoService;
        }

        public void Processar()
        {
            _logger.Info()
                .Message("Iniciado o processamento da {}", Constants.INTEGRACAO_BB)
                .Property("application", Constants.SISTEMA_CAMADA_TASKS)
                .Write();

            // var ordens = ObterChamadosArquivo();

            // ordens.ForEach(os => {
            //     AbreChamado(os);
            // });
        }

        public List<OrdemServico> ObterChamadosArquivo()
        {
            List<OrdemServico> ordens = new();

            // Ler o arquivo
            string linha = "120237816000650000MANUTENCAO781613SOP-JD.CAMBURI      08:00:00/18:00:0027   33376555000000000000191R.VICTORINO CARDOSO,235            JARDIM CAMBURI           VITORIA                     ES02909082008PLANTAO DIAS UTEIS, DE 8H AS 18H. SOLUCAO EM 30H  7417221520162TAA-TERMINAL DE AUTOATENDIMENTO    7417221520162-8  0                      036500URGENTE   LEITORA DE CARTíES COM DEFEITO                                                                                                    0001MANUT. CORRETIVA    400306444    PERTO SA PERIFERICOS PARA AUTOMACAO     F681193720062023161010F6811937MARCOS      SAA                                               20062023161010        0000000001202100000467          10000000000000000000005                                                                                                   N";

            OrdemServicoBB osCliente = new OrdemServicoBB {
               NumOSCliente = linha.Substring(2, 15),
               TipoOS = linha.Substring(19, 28),
               NumAgencia = linha.Substring(29, 32)?.PadLeft(5),
               DCPosto = linha.Substring(33, 34)?.PadLeft(2),
               NomeAgencia = linha.Substring(35, 54),
               HoraInicial = linha.Substring(55, 71),
               HoraFinal = linha.Substring(55, 71),
               DDDDependencia = linha.Substring(72, 75),
               Telefone = linha.Substring(76, 84),
               CGC = linha.Substring(85, 99),
               Endereco = linha.Substring(100, 134),
               Bairro = linha.Substring(135, 159),
               Cidade = linha.Substring(160, 187),
               UF = linha.Substring(188, 189),
               CEP = linha.Substring(190, 198),
               Criticidade = linha.Substring(199, 200),
               DescCriticidade = linha.Substring(201, 250),
               NumBem = linha.Substring(251, 263),
               DescricaoBem = linha.Substring(264, 298),
               NumSerie = linha.Substring(299, 315),
               Modelo = linha.Substring(316, 325),
               BilheteOS = linha.Substring(326, 338),
               GarantiaBem = linha.Substring(339, 342),
               Impacto = linha.Substring(343, 344),
               DescricaoImpacto = linha.Substring(345, 354),
               Defeito = linha.Substring(355, 484),
               TipoManutencao = linha.Substring(485, 488),
               DescricaoManutencao = linha.Substring(489, 508),
               CodigoFornecedor = linha.Substring(509, 521),
               DescricaoFornecedor = linha.Substring(522, 561),
               MatriculaAberturaOS = linha.Substring(562, 569),
               DataAberturaOS = linha.Substring(570, 577),
               HoraAberturaOS = linha.Substring(578, 583),
               NomeContato = linha.Substring(584, 603),
               NumeroChamada = linha.Substring(644, 653),
               DataChamadaOS = linha.Substring(654, 661),
               HoraChamadaOS = linha.Substring(662, 667),
               ChaveDoChamador = linha.Substring(668, 675),
               NumeroDiasAtendimento = linha.Substring(676, 679),
               NumeroHorasAtendimento = linha.Substring(680, 685),
               NumeroContrato = linha.Substring(686, 697),
               CodigoMantenedora = linha.Substring(698, 707),
               IndOSGarantia = linha.Substring(708, 708),
               DataAgendaOS = linha.Substring(709, 716),
               HoraAgendaOS = linha.Substring(717, 722),
               PrefixoDependenciaDestino = linha.Substring(723, 726),
               SubordinadaDependenciaDestino = linha.Substring(727, 728),
               CodigoTipoBem = linha.Substring(729, 730),
               TextoMotivoManutencao = linha.Substring(731, 830)
            };

            EquipamentoContrato equipamentoContrato = (EquipamentoContrato)_equipamentoContratoService
                .ObterPorParametros(new EquipamentoContratoParameters {
                    NumSerie = osCliente.NumSerie,
                    CodClientes = Constants.CLIENTE_BB.ToString()
                })
                .Items.FirstOrDefault();

            if (equipamentoContrato == null) 
                throw new Exception("Não encontrou equipamento");

            LocalAtendimento localAtendimento = (LocalAtendimento)_localAtendimentoService
                .ObterPorParametros(new LocalAtendimentoParameters {
                    NumAgencia = osCliente.NumAgencia,
                    DCPosto = osCliente.DCPosto,
                    CodCliente = Constants.CLIENTE_BB
                })
                .Items.FirstOrDefault();

            bool isOSAberta = _ordemServicoService.ObterPorParametros(new OrdemServicoParameters {
                NumOSCliente = osCliente.NumOSCliente,
                CodCliente = Constants.CLIENTE_BB
            }).Items.FirstOrDefault() != null;

            if (isOSAberta)
                throw new Exception("OS ja aberta");

            if (localAtendimento == null) 
                throw new Exception("Não encontrou local");

            var observacaoCliente = $@"
                OS INTEGRADA DIA { DateTime.Now.ToString() }
                SÉRIE: { equipamentoContrato.NumSerie }
                AGÊNCIA: { localAtendimento.NumAgencia }/{ localAtendimento.DCPosto } - { localAtendimento.NomeLocal }
                ENDEREÇO: { osCliente.Endereco }
                BAIRRO: { osCliente.Bairro }
                UF: { osCliente.UF }
                CIDADE: { osCliente.Cidade }
                CEP: { osCliente.CEP }
            ";

            var ordemServico = new OrdemServico {
                CodCliente = Constants.CLIENTE_BB,
                CodPosto = (int)localAtendimento.CodPosto,
                CodEquipContrato = equipamentoContrato.CodEquipContrato,
                NomeContato = osCliente.NomeContato,
                DefeitoRelatado = osCliente.Defeito,
                TelefoneSolicitante = osCliente.Telefone,
                NomeSolicitante = osCliente.NomeContato,
                ObservacaoCliente = observacaoCliente,
                IndServico = 1,
                IndIntegracao = 1,
                DataHoraCad = DateTime.Now,
                DataHoraAberturaOS = DateTime.Now,
                CodUsuarioCad = "SAT",
                CodStatusServico = Constants.STATUS_SERVICO_ABERTO,
                CodTipoIntervencao = Constants.CORRETIVA,
                CodEquip = equipamentoContrato.CodEquip,
                CodRegiao = equipamentoContrato.CodRegiao,
                CodAutorizada = equipamentoContrato.CodAutorizada,
                CodFilial = equipamentoContrato.CodFilial
            };

            ordens.Add(ordemServico);

            return ordens;
        }

        public void AbreChamado(OrdemServico os) 
        {

        }
    }
}