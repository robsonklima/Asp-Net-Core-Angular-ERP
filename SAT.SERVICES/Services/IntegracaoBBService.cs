using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NLog;
using NLog.Fluent;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using SAT.UTILS;

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

        public Task ProcessarsAsync()
        {
            string linha = "120237816000650000MANUTENCAO781613SOP-JD.CAMBURI      08:00:00/18:00:0027   33376555000000000000191R.VICTORINO CARDOSO,235            JARDIM CAMBURI           VITORIA                     ES02909082008PLANTAO DIAS UTEIS, DE 8H AS 18H. SOLUCAO EM 30H  7417221520162TAA-TERMINAL DE AUTOATENDIMENTO    7417221520162-8  0                      036500URGENTE   LEITORA DE CARTíES COM DEFEITO                                                                                                    0001MANUT. CORRETIVA    400306444    PERTO SA PERIFERICOS PARA AUTOMACAO     F681193720062023161010F6811937MARCOS      SAA                                               20062023161010        0000000001202100000467          10000000000000000000005                                                                                                   N";
            string numOSCliente = linha.Substring(2, 15);
            string tipoOS = linha.Substring(19, 28);
            string numAgencia = linha.Substring(29, 32)?.PadLeft(5);
            string dcPosto = linha.Substring(33, 34)?.PadLeft(2);
            string nomeAgencia = linha.Substring(35, 54);
            string horaInicial = linha.Substring(55, 71);
            string horaFinal = linha.Substring(55, 71);
            string dddDependencia = linha.Substring(72, 75);
            string telefone = linha.Substring(76, 84);
            string cgc = linha.Substring(85, 99);
            string endereco = linha.Substring(100, 134);
            string bairro = linha.Substring(135, 159);
            string cidade = linha.Substring(160, 187);
            string uf = linha.Substring(188, 189);
            string cep = linha.Substring(190, 198);
            string criticidade = linha.Substring(199, 200);
            string descCriticidade = linha.Substring(201, 250);
            string numBem = linha.Substring(251, 263);
            string descricaoBem = linha.Substring(264, 298);
            string numSerie = linha.Substring(299, 315);
            string modelo = linha.Substring(316, 325);
            string bilheteOS = linha.Substring(326, 338);
            string garantiaBem = linha.Substring(339, 342);
            string impacto = linha.Substring(343, 344);
            string descricaoImpacto = linha.Substring(345, 354);
            string defeito = linha.Substring(355, 484);
            string tipoManutencao = linha.Substring(485, 488);
            string descricaoManutencao = linha.Substring(489, 508);
            string codigoFornecedor = linha.Substring(509, 521);
            string descricaoFornecedor = linha.Substring(522, 561);
            string matriculaAberturaOS = linha.Substring(562, 569);
            string dataAberturaOS = linha.Substring(570, 577);
            string horaAberturaOS = linha.Substring(578, 583);
            string nomeContato = linha.Substring(584, 603);
            string numeroChamada = linha.Substring(644, 653);
            string dataChamadaOS = linha.Substring(654, 661);
            string horaChamadaOS = linha.Substring(662, 667);
            string chaveDoChamador = linha.Substring(668, 675);
            string numeroDiasAtendimento = linha.Substring(676, 679);
            string numeroHorasAtendimento = linha.Substring(680, 685);
            string numeroContrato = linha.Substring(686, 697);
            string codigoMantenedora = linha.Substring(698, 707);
            string IndOSGarantia = linha.Substring(708, 708);
            string dataAgendaOS = linha.Substring(709, 716);
            string horaAgendaOS = linha.Substring(717, 722);
            string prefixoDependenciaDestino = linha.Substring(723, 726);
            string subordinadaDependenciaDestino = linha.Substring(727, 728);
            string codigoTipoBem = linha.Substring(729, 730);
            string textoMotivoManutencao = linha.Substring(731, 830);

            EquipamentoContrato equipamentoContrato = (EquipamentoContrato)_equipamentoContratoService
                .ObterPorParametros(new EquipamentoContratoParameters {
                    NumSerie = numSerie,
                    CodClientes = Constants.CLIENTE_BB.ToString()
                })
                .Items.FirstOrDefault();

            if (equipamentoContrato == null) 
                return null;

            LocalAtendimento localAtendimento = (LocalAtendimento)_localAtendimentoService
                .ObterPorParametros(new LocalAtendimentoParameters {
                    NumAgencia = numAgencia,
                    DCPosto = dcPosto,
                    CodCliente = Constants.CLIENTE_BB
                })
                .Items.FirstOrDefault();

            bool isOSAberta = _ordemServicoService.ObterPorParametros(new OrdemServicoParameters {
                NumOSCliente = numOSCliente,
                CodCliente = Constants.CLIENTE_BB
            }).Items.FirstOrDefault() != null;

            if (isOSAberta)
                return null;

            if (localAtendimento == null) 
                throw new Exception("Não encontrou local");

            var observacaoCliente = $@"
                OS INTEGRADA DIA { DateTime.Now.ToString() }
                SÉRIE: { equipamentoContrato.NumSerie }
                AGÊNCIA: { localAtendimento.NumAgencia }/{ localAtendimento.DCPosto } - { localAtendimento.NomeLocal }
                ENDEREÇO: { endereco }
                BAIRRO: { bairro }
                UF: { uf }
                CIDADE: { cidade }
                CEP: { cep }
            ";

            var ordemServico = new OrdemServico {
                CodCliente = Constants.CLIENTE_BB,
                CodPosto = (int)localAtendimento.CodPosto,
                CodEquipContrato = equipamentoContrato.CodEquipContrato,
                NomeContato = nomeContato,
                DefeitoRelatado = defeito,
                TelefoneSolicitante = telefone,
                NomeSolicitante = nomeContato,
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
            
            return null;
        }
    }
}