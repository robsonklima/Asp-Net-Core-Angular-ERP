using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            _logger.Info($"Iniciando o procesamento: { Constants.INTEGRACAO_BB }");

            List<string> files = LerDiretorio();

            _logger.Info($"Encontrados: { files.Count() } para processamento");

            files.ForEach((file) => {
                var chamados = ExtrairChamadosArquivo(file);

                chamados.ForEach(chamado => {
                    //var os = _ordemServicoService.Criar(chamado);
                    //_logger.Info($"Aberto chamado: { os.CodOS }");
                    CriarArquivoRetorno(chamado);
                });
            });
        }

        public List<OrdemServico> ExtrairChamadosArquivo(string conteudo)
        {
            List<OrdemServico> ordens = new();

            OrdemServicoBB osCliente = new OrdemServicoBB
            {
                NumOSCliente = conteudo.Substring(2, 15),
                TipoOS = conteudo.Substring(19, 28),
                NumAgencia = conteudo.Substring(29, 32)?.PadLeft(5),
                DCPosto = conteudo.Substring(33, 34)?.PadLeft(2),
                NomeAgencia = conteudo.Substring(35, 54),
                HoraInicial = conteudo.Substring(55, 71),
                HoraFinal = conteudo.Substring(55, 71),
                DDDDependencia = conteudo.Substring(72, 75),
                Telefone = conteudo.Substring(76, 84),
                CGC = conteudo.Substring(85, 99),
                Endereco = conteudo.Substring(100, 134),
                Bairro = conteudo.Substring(135, 159),
                Cidade = conteudo.Substring(160, 187),
                UF = conteudo.Substring(188, 189),
                CEP = conteudo.Substring(190, 198),
                Criticidade = conteudo.Substring(199, 200),
                DescCriticidade = conteudo.Substring(201, 250),
                NumBem = conteudo.Substring(251, 263),
                DescricaoBem = conteudo.Substring(264, 298),
                NumSerie = conteudo.Substring(299, 315),
                Modelo = conteudo.Substring(316, 325),
                BilheteOS = conteudo.Substring(326, 338),
                GarantiaBem = conteudo.Substring(339, 342),
                Impacto = conteudo.Substring(343, 344),
                DescricaoImpacto = conteudo.Substring(345, 354),
                Defeito = conteudo.Substring(355, 484),
                TipoManutencao = conteudo.Substring(485, 488),
                DescricaoManutencao = conteudo.Substring(489, 508),
                CodigoFornecedor = conteudo.Substring(509, 521),
                DescricaoFornecedor = conteudo.Substring(522, 561),
                MatriculaAberturaOS = conteudo.Substring(562, 569),
                DataAberturaOS = conteudo.Substring(570, 577),
                HoraAberturaOS = conteudo.Substring(578, 583),
                NomeContato = conteudo.Substring(584, 603),
                NumeroChamada = conteudo.Substring(644, 653),
                DataChamadaOS = conteudo.Substring(654, 661),
                HoraChamadaOS = conteudo.Substring(662, 667),
                ChaveDoChamador = conteudo.Substring(668, 675),
                NumeroDiasAtendimento = conteudo.Substring(676, 679),
                NumeroHorasAtendimento = conteudo.Substring(680, 685),
                NumeroContrato = conteudo.Substring(686, 697),
                CodigoMantenedora = conteudo.Substring(698, 707),
                IndOSGarantia = conteudo.Substring(708, 708),
                DataAgendaOS = conteudo.Substring(709, 716),
                HoraAgendaOS = conteudo.Substring(717, 722),
                PrefixoDependenciaDestino = conteudo.Substring(723, 726),
                SubordinadaDependenciaDestino = conteudo.Substring(727, 728),
                CodigoTipoBem = conteudo.Substring(729, 730),
                TextoMotivoManutencao = conteudo.Substring(731, 830)
            };

            EquipamentoContrato equipamentoContrato = (EquipamentoContrato)_equipamentoContratoService
                .ObterPorParametros(new EquipamentoContratoParameters
                {
                    NumSerie = osCliente.NumSerie,
                    CodClientes = Constants.CLIENTE_BB.ToString()
                })
                .Items.FirstOrDefault();

            if (equipamentoContrato == null)
                throw new Exception("Não encontrou equipamento");

            LocalAtendimento localAtendimento = (LocalAtendimento)_localAtendimentoService
                .ObterPorParametros(new LocalAtendimentoParameters
                {
                    NumAgencia = osCliente.NumAgencia,
                    DCPosto = osCliente.DCPosto,
                    CodCliente = Constants.CLIENTE_BB
                })
                .Items.FirstOrDefault();

            bool isOSAberta = _ordemServicoService.ObterPorParametros(new OrdemServicoParameters
            {
                NumOSCliente = osCliente.NumOSCliente,
                CodCliente = Constants.CLIENTE_BB
            }).Items.FirstOrDefault() != null;

            if (isOSAberta)
                throw new Exception("OS ja aberta");

            if (localAtendimento == null)
                throw new Exception("Não encontrou local");

            var observacaoCliente = $@"
                OS INTEGRADA DIA {DateTime.Now.ToString()}
                SÉRIE: {equipamentoContrato.NumSerie}
                AGÊNCIA: {localAtendimento.NumAgencia}/{localAtendimento.DCPosto} - {localAtendimento.NomeLocal}
                ENDEREÇO: {osCliente.Endereco}
                BAIRRO: {osCliente.Bairro}
                UF: {osCliente.UF}
                CIDADE: {osCliente.Cidade}
                CEP: {osCliente.CEP}
            ";

            var ordemServico = new OrdemServico
            {
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

        public void CriarArquivoRetorno(OrdemServico chamado)
        {
            string fileName = "CRM549R.xPerto01." + DateTime.Now.ToString("ddMMyyyyHHMMsss");
            string target = Directory.GetCurrentDirectory() + "/Output";

            using (StreamWriter w = new StreamWriter(target))
            {
                w.WriteLine("Teste 1");
                w.WriteLine("Teste 2");
                w.WriteLine("Teste 3");
            }
        }

        public List<string> LerDiretorio()
        {
            string target = Directory.GetCurrentDirectory() + "/Input";
            DirectoryInfo dirInfo = new DirectoryInfo(target);
            FileInfo[] files = dirInfo.GetFiles("crm549.*.BB");
            List<string> linhas = new();

            foreach (FileInfo file in files)
            {
                using (StreamReader sr = File.OpenText(target + "/" + file.Name))
                {
                    string linha = String.Empty;

                    while ((linha = sr.ReadLine()) is not null)
                    {
                        char primeiroCaractere = linha[0];

                        if (primeiroCaractere == '1')
                        {
                            linhas.Add(linha);
                        }
                    }
                }
            }

            return linhas;
        }
    }
}