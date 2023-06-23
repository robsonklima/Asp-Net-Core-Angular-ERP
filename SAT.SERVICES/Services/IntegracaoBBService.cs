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
                OrdemServico chamadoPerto = AbrirChamadoCliente(chamadoCliente);
                RegistrarChamadoClienteNoLog(chamadoCliente);
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
                NumOSCliente = conteudo.Substring(2, 14),
                TipoOS = conteudo.Substring(19, 3),
                NumAgencia = String.Format("{0:00000}", conteudo.Substring(29, 4)),
                DCPosto = String.Format("{0:00}", conteudo.Substring(33, 2)),
                NomeAgencia = conteudo.Substring(35, 20),
                HoraInicial = conteudo.Substring(55, 17),
                HoraFinal = conteudo.Substring(55, 17),
                DDDDependencia = conteudo.Substring(72, 3),
                Telefone = conteudo.Substring(76, 10),
                CGC = conteudo.Substring(85, 15),
                Endereco = conteudo.Substring(100, 35),
                Bairro = conteudo.Substring(135, 25),
                Cidade = conteudo.Substring(160, 28),
                UF = conteudo.Substring(188, 2),
                CEP = conteudo.Substring(190, 8),
                Criticidade = conteudo.Substring(199, 2),
                DescCriticidade = conteudo.Substring(201, 50),
                NumBem = conteudo.Substring(251, 13),
                DescricaoBem = conteudo.Substring(264, 35),
                NumSerie = conteudo.Substring(299, 17),
                Modelo = conteudo.Substring(316, 10),
                BilheteOS = conteudo.Substring(326, 13),
                GarantiaBem = conteudo.Substring(339, 4),
                Impacto = conteudo.Substring(343,2),
                DescricaoImpacto = conteudo.Substring(345, 10),
                Defeito = conteudo.Substring(355, 129),
                TipoManutencao = conteudo.Substring(485, 4),
                DescricaoManutencao = conteudo.Substring(489, 20),
                CodigoFornecedor = conteudo.Substring(509, 13),
                DescricaoFornecedor = conteudo.Substring(522, 40),
                MatriculaAberturaOS = conteudo.Substring(562, 8),
                DataAberturaOS = conteudo.Substring(570, 8),
                HoraAberturaOS = conteudo.Substring(578, 6),
                NomeContato = conteudo.Substring(584, 20),
                NumeroChamada = conteudo.Substring(644, 10),
                DataChamadaOS = conteudo.Substring(654, 8),
                HoraChamadaOS = conteudo.Substring(662, 6),
                ChaveDoChamador = conteudo.Substring(668, 8),
                NumeroDiasAtendimento = conteudo.Substring(676, 4),
                NumeroHorasAtendimento = conteudo.Substring(680, 6),
                NumeroContrato = conteudo.Substring(686, 12),
                CodigoMantenedora = conteudo.Substring(698, 10),
                IndOSGarantia = conteudo.Substring(708, 1),
                DataAgendaOS = conteudo.Substring(709, 8),
                HoraAgendaOS = conteudo.Substring(717, 6),
                PrefixoDependenciaDestino = conteudo.Substring(723, 4),
                SubordinadaDependenciaDestino = conteudo.Substring(727, 2),
                CodigoTipoBem = conteudo.Substring(729, 2),
                TextoMotivoManutencao = conteudo.Substring(731, 100)
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

        private string MontarLinhaArquivo(OrdemServico chamado)
        {
            _logger.Info($"Iniciando a composição de nova linha no arquivo");

            string numOSCliente = chamado.NumOSCliente;
            string dataAbertura = chamado.DataHoraCad.Value.ToString("DDMMyyyy");
            string horaAbertura = chamado.DataHoraCad.Value.ToString("HHMM");
            string codOS = chamado.CodOS.ToString();

            _logger.Info($"Finalizando a composição de nova linha no arquivo");

            return $"3{numOSCliente} {dataAbertura} {horaAbertura}000{codOS}00";
        }

        private string MontarCabecalhoArquivoFechamento(List<OrdemServico> chamados)
        {
            _logger.Info($"Iniciando a composição do cabeçalho do arquivo de fechamento");

            string horaAtual = DateTime.Now.ToString("HHmms");
            string dataGeracao = DateTime.Now.ToString("DDMMyyy");
            string horaGeracao = DateTime.Now.ToString("HHmms");
            string qtdChamados = String.Format("{0:00000}", chamados.Count());

            _logger.Info($"Iniciando a composição do cabeçalho do arquivo de fechamento");

            return $"2{dataGeracao} {horaGeracao}3{qtdChamados}crm549R       ";
        }

        private void CriarArquivoRetornoFechamento(List<OrdemServico> chamados)
        {
            _logger.Info($"Iniciando a criação dos arquivos de retorno");

            var parameters = new IntegracaoBBParameters { };
            var integracoes = _integracaoBBRepo.ObterPorParametros(parameters);

            foreach (var integracao in integracoes)
            {
                string fileName = "CRM549R.xPerto01." + DateTime.Now.ToString("ddMMyyyyHHMMsss");
                string target = Directory.GetCurrentDirectory() + "/Output";

                using (StreamWriter w = new StreamWriter(target))
                {
                    string cabecalho = MontarCabecalhoArquivoFechamento(chamados);

                    w.WriteLine(cabecalho);

                    chamados.ForEach(chamado =>
                    {
                        string linha = MontarLinhaArquivo(chamado);

                        w.WriteLine(linha);
                    });
                }
            }

            _logger.Info($"Finalizando a criação dos arquivos de retorno");
        }

        private string NormalizarConteudo(string conteudo)
        {
            Regex reg = new Regex("[*'\",_&#^@]");

            return reg.Replace(conteudo, string.Empty);
        }

        private void RegistrarChamadoClienteNoLog(OrdemServicoBB chamado)
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