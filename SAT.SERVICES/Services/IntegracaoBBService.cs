using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using NLog;
using SAT.MODELS.Views;

namespace SAT.SERVICES.Services
{
    public partial class IntegracaoBBService : IIntegracaoBBService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private IOrdemServicoService _ordemServicoService;
        private ILocalAtendimentoService _localAtendimentoService;
        private IEquipamentoContratoService _equipamentoContratoService;
        private IIntegracaoBBRepository _integracaoBBRepo;

        public IntegracaoBBService(
            IOrdemServicoService ordemServicoService,
            ILocalAtendimentoService localAtendimentoService,
            IEquipamentoContratoService equipamentoContratoService,
            IIntegracaoBBRepository integracaoBBRepo
        )
        {
            _ordemServicoService = ordemServicoService;
            _localAtendimentoService = localAtendimentoService;
            _equipamentoContratoService = equipamentoContratoService;
            _integracaoBBRepo = integracaoBBRepo;
        }

        public void Processar()
        {
            _logger.Info($"Iniciando o processamento: {Constants.INTEGRACAO_BB}");

            List<string> files = LerDiretorioInput();

            _logger.Info($"Encontrados: {files.Count()} arquivos para processamento");

            files.ForEach((file) =>
            {
                string conteudoNormalizado = NormalizarConteudo(file);
                OrdemServicoBB chamadoCliente = ExtrairChamadoArquivoAbertura(conteudoNormalizado);
                //OrdemServico chamadoPerto = AbrirChamadoCliente(chamadoCliente);
                RegistrarLogChamadoCliente(chamadoCliente);
                //CriarArquivoRetornoAbertura(chamados);
                //CriarArquivoRetornoFechamento(chamados);
            });
        }

        private List<string> LerDiretorioInput()
        {
            string target = Directory.GetCurrentDirectory() + "/Input";

            _logger.Info($"Lendo diretorio de arquivos para processamento: {target}");

            DirectoryInfo dirInfo = new DirectoryInfo(target);
            string nomenclatura = "crm549.*.BB";
            FileInfo[] files = dirInfo.GetFiles(nomenclatura);

            _logger.Info($"Lendo arquivos do tipo: {nomenclatura}");

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

                            _logger.Info($"Lendo a linha: {linha}");
                        }
                    }
                }
            }

            return linhas;
        }

        private OrdemServicoBB ExtrairChamadoArquivoAbertura(string conteudo)
        {
            if (conteudo.Length < Constants.INT_BB_TAMANHO_ARQUIVO)
            {
                _logger.Info($"Conteudo do arquivo menor que o especificado: {Constants.INT_BB_TAMANHO_ARQUIVO} caracteres");

                return null;
            }

            OrdemServicoBB osCliente = new OrdemServicoBB
            {
                NumOSCliente = conteudo.Substring(1, 14),
                TipoOS = conteudo.Substring(18, 3),
                NumAgencia = String.Format("{0:00000}", conteudo.Substring(28, 4)),
                DCPosto = String.Format("{0:00}", conteudo.Substring(32, 2)),
                NomeAgencia = conteudo.Substring(34, 20),
                HoraInicial = conteudo.Substring(54, 17),
                HoraFinal = conteudo.Substring(54, 17),
                DDDDependencia = conteudo.Substring(71, 3),
                Telefone = conteudo.Substring(75, 10),
                CGC = conteudo.Substring(84, 15),
                Endereco = conteudo.Substring(99, 35),
                Bairro = conteudo.Substring(134, 25),
                Cidade = conteudo.Substring(159, 28),
                UF = conteudo.Substring(187, 2),
                CEP = conteudo.Substring(189, 8),
                Criticidade = conteudo.Substring(198, 2),
                DescCriticidade = conteudo.Substring(200, 50),
                NumBem = conteudo.Substring(250, 13),
                DescricaoBem = conteudo.Substring(263, 35),
                NumSerie = conteudo.Substring(298, 17),
                Modelo = conteudo.Substring(315, 10),
                BilheteOS = conteudo.Substring(325, 13),
                GarantiaBem = conteudo.Substring(338, 4),
                Impacto = conteudo.Substring(342,2),
                DescricaoImpacto = conteudo.Substring(344, 10),
                Defeito = conteudo.Substring(354, 129),
                TipoManutencao = conteudo.Substring(484, 4),
                DescricaoManutencao = conteudo.Substring(488, 20),
                CodigoFornecedor = conteudo.Substring(508, 13),
                DescricaoFornecedor = conteudo.Substring(521, 40),
                MatriculaAberturaOS = conteudo.Substring(561, 8),
                DataAberturaOS = conteudo.Substring(569, 8),
                HoraAberturaOS = conteudo.Substring(577, 6),
                NomeContato = conteudo.Substring(583, 20),
                NumeroChamada = conteudo.Substring(643, 10),
                DataChamadaOS = conteudo.Substring(653, 8),
                HoraChamadaOS = conteudo.Substring(661, 6),
                ChaveDoChamador = conteudo.Substring(667, 8),
                NumeroDiasAtendimento = conteudo.Substring(675, 4),
                NumeroHorasAtendimento = conteudo.Substring(679, 6),
                NumeroContrato = conteudo.Substring(685, 12),
                CodigoMantenedora = conteudo.Substring(697, 10),
                IndOSGarantia = conteudo.Substring(707, 1),
                DataAgendaOS = conteudo.Substring(708, 8),
                HoraAgendaOS = conteudo.Substring(716, 6),
                PrefixoDependenciaDestino = conteudo.Substring(722, 4),
                SubordinadaDependenciaDestino = conteudo.Substring(726, 2),
                CodigoTipoBem = conteudo.Substring(728, 2),
                TextoMotivoManutencao = conteudo.Substring(730, 100)
            };

            return osCliente;
        }

        private void CriarArquivoRetornoAbertura(List<OrdemServico> chamados)
        {
            _logger.Info($"Iniciando a extração dos chamados nos arquivos");

            string fileName = "CRM549R.xPerto01." + DateTime.Now.ToString("ddMMyyyyHHMMsss");
            string target = Directory.GetCurrentDirectory() + "/Output";

            _logger.Info($"Lendo diretorio arquivos para processamento: {target}");

            using (StreamWriter w = new StreamWriter(target))
            {
                string cabecalho = MontarCabecalhoArquivoAbertura(chamados);

                w.WriteLine(cabecalho);
                _logger.Info($"Adicionado o cabecalho: {cabecalho}");

                chamados.ForEach(chamado =>
                {
                    string linha = MontarLinhaArquivo(chamado);

                    w.WriteLine(linha);

                    _logger.Info($"Adicionada a linha: {linha}");
                });
            }

            _logger.Info($"Finalizando a extração dos chamados nos arquivos");
        }

        private string MontarCabecalhoArquivoAbertura(List<OrdemServico> chamados)
        {
            _logger.Info($"Iniciando a composição do cabeçalho do arquivo de abertura");

            string horaAtual = DateTime.Now.ToString("HHmms");
            string dataGeracao = DateTime.Now.ToString("DDMMyyy");
            string horaGeracao = DateTime.Now.ToString("HHmms");
            string qtdChamados = String.Format("{0:00000}", chamados.Count());

            _logger.Info($"Finalizando a composição do cabeçalho do arquivo de abertura");

            return $"2{dataGeracao} {horaGeracao}3{qtdChamados}crm549R       ";
        }

        private OrdemServico AbrirChamadoCliente(OrdemServicoBB chamadoCliente)
        {
            EquipamentoContrato equipamentoContrato = (EquipamentoContrato)_equipamentoContratoService
                .ObterPorParametros(new EquipamentoContratoParameters
                {
                    NumSerie = chamadoCliente.NumSerie,
                    CodClientes = Constants.CLIENTE_BB.ToString()
                })
                .Items.FirstOrDefault();

            if (equipamentoContrato == null)
            {
                _logger.Info("Equipamento informado nao encontrado");

                return null;
            }

            LocalAtendimento localAtendimento = (LocalAtendimento)_localAtendimentoService
                .ObterPorParametros(new LocalAtendimentoParameters
                {
                    NumAgencia = chamadoCliente.NumAgencia,
                    DCPosto = chamadoCliente.DCPosto,
                    CodCliente = Constants.CLIENTE_BB
                })
                .Items.FirstOrDefault();

            bool isOSAberta = _ordemServicoService.ObterPorParametros(new OrdemServicoParameters {
                CodCliente = Constants.CLIENTE_BB,
                NumOSCliente = chamadoCliente.NumOSCliente
            }).Items.FirstOrDefault() != null;

            if (isOSAberta)
            {
                _logger.Info($"Esta ordem de servico ja foi aberta: Num OS Cliente { chamadoCliente.NumOSCliente }");

                return null;
            }

            if (localAtendimento == null)
                _logger.Info($"Local informado nao pode ser encontrado { chamadoCliente.NumAgencia }/{ chamadoCliente.DCPosto } {chamadoCliente.NomeAgencia}");

            var observacaoCliente = $@"
                    OS INTEGRADA DIA {DateTime.Now.ToString()}
                    SÉRIE: {equipamentoContrato.NumSerie}
                    AGÊNCIA: {localAtendimento.NumAgencia}/{localAtendimento.DCPosto} - {localAtendimento.NomeLocal}
                    ENDEREÇO: {chamadoCliente.Endereco}
                    BAIRRO: {chamadoCliente.Bairro}
                    UF: {chamadoCliente.UF}
                    CIDADE: {chamadoCliente.Cidade}
                    CEP: {chamadoCliente.CEP}
                ";

            var ordemServico = new OrdemServico
            {
                CodCliente = Constants.CLIENTE_BB,
                CodPosto = (int)localAtendimento.CodPosto,
                CodEquipContrato = equipamentoContrato.CodEquipContrato,
                NomeContato = chamadoCliente.NomeContato,
                DefeitoRelatado = chamadoCliente.Defeito,
                TelefoneSolicitante = chamadoCliente.Telefone,
                NomeSolicitante = chamadoCliente.NomeContato,
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

            OrdemServico novaOrdemServico = _ordemServicoService.Criar(ordemServico);

            _logger.Info($"Ordem de servico criada com sucesso: { ordemServico.CodOS }");

            return novaOrdemServico;
        }

        private string MontarLinha1ArquivoFechamento(ViewIntegracaoBB chamado)
        {
            _logger.Info($"Iniciando a composição de nova linha no arquivo de fechamento");
            string numRAT = String.Format("{0:0000000000}", chamado.NumRAT);
            string horaGeracao = DateTime.Now.ToString("HHmms");

            string retorno = @$"{ chamado.NumOSCliente }1{ chamado.DataInicioAtendimento }{ chamado.HoraInicioAtendimento }{ chamado.DataFimAtendimento }{ chamado.HoraFimAtendimento }{ chamado.DataFinal }{ chamado.HoraFinal }{ numRAT }{ chamado.NomeTecnico }000{ chamado.DeParaCausa }0000{ chamado.SituacaoOS }01{ chamado.DataAgendamento }{ chamado.HoraAgendamento }{ horaGeracao }      00002";


            //string retorno = @$"202378180005252AAA007       00000000000000000000000000000                                                         00000      00003
            //                    2023781800052530090FECHAMENTO                                                                                     00153      00004";                                

            _logger.Info($"Finalizando a composição de nova linha no arquivo de fechamento");

            return $"";
        }

        private string MontarCabecalhoArquivoFechamento()
        {
            _logger.Info($"Iniciando a composição do cabeçalho do arquivo de fechamento");

            string data = DateTime.Now.ToString("DDMMyyyy");
            string hora = DateTime.Now.ToString("HHMMs");

            string retorno = @$"000000000000000{ hora }CRM558A400306444                                                                                 { data }00001";

            _logger.Info($"Iniciando a composição do cabeçalho do arquivo de fechamento: { retorno }");

            return retorno;
        }

        private string MontarRodapeArquivoFechamento()
        {
            _logger.Info($"Iniciando a composição do rodape do arquivo de fechamento");

            string hora = DateTime.Now.ToString("HHMMs");

            string retorno = @$"999999999999999{ hora }                                                                                          00000000000000400000";

            _logger.Info($"Iniciando a composição do rodape do arquivo de fechamento");

            return retorno;
        }

        private void CriarArquivoFechamento()
        {
            _logger.Info($"Iniciando a criação dos arquivos de retorno");

            var parameters = new IntegracaoBBParameters { };
            var chamados = _integracaoBBRepo.ObterPorParametros(parameters);

            foreach (var chamado in chamados)
            {
                string fileName = "crm558a.xperto01." + DateTime.Now.ToString("ddMMyyyyHHMMsss") + ".bco001";
                string target = Directory.GetCurrentDirectory() + "/Output";

                using (StreamWriter w = new StreamWriter(target))
                {
                    string cabecalho = MontarCabecalhoArquivoFechamento();
                    string rodape = MontarRodapeArquivoFechamento();

                    w.WriteLine(cabecalho);

                    chamados.ForEach(chamado =>
                    {
                        string linha = MontarLinhaArquivoFechamento(chamado);

                        w.WriteLine(linha);
                    });

                    w.WriteLine(rodape);
                }
            }

            _logger.Info($"Finalizando a criação dos arquivos de retorno");
        }

        private string NormalizarConteudo(string conteudo)
        {
            Regex reg = new Regex("[*'\",_&#^@]");

            return reg.Replace(conteudo, " ");
        }

        private void RegistrarLogChamadoCliente(OrdemServicoBB chamado)
        {
            _logger.Info($"Iniciando o registro de logs do conteudo do arquivo do cliente");

            string conteudo = string.Empty;

            foreach (PropertyInfo prop in chamado.GetType().GetProperties())
            {
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                if (type == typeof(DateTime))
                {
                    string valor = prop.GetValue(chamado, null).ToString();

                    if (!string.IsNullOrWhiteSpace(valor))
                        conteudo += $"{conteudo}; {type}: {valor}";
                }
            }

            _logger.Info($"Obtendo conteudo do arquivo do cliente: {conteudo}");
        }
    }
}