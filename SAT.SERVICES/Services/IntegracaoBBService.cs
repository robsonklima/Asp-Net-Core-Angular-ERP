using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using NLog;
using SAT.INFRA.Interfaces;
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

            List<string> files = LerDiretorio();

            _logger.Info($"Encontrados: {files.Count()} arquivos para processamento");

            files.ForEach((file) =>
            {
                var chamados = ExtrairChamadosArquivo(NormalizarConteudo(file));

                //CriarArquivoRetornoAbertura(chamados);
                //CriarArquivoRetornoFechamento(chamados);
            });
        }

        private List<string> LerDiretorio()
        {
            string target = Directory.GetCurrentDirectory() + "/Input";

            _logger.Info($"Lendo diretorio de arquivos para processamento: { target }");

            DirectoryInfo dirInfo = new DirectoryInfo(target);
            string nomenclatura = "crm549.*.BB";
            FileInfo[] files = dirInfo.GetFiles(nomenclatura);

            _logger.Info($"Lendo arquivos do tipo: { nomenclatura }");

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

                            _logger.Info($"Lendo a linha: { linha }");
                        }
                    }
                }
            }

            return linhas;
        }

        private List<OrdemServico> ExtrairChamadosArquivo(string conteudo)
        {
            if (conteudo.Length < Constants.INT_BB_TAMANHO_ARQUIVO) {
                _logger.Info($"Conteudo do arquivo menor que o especificado: { Constants.INT_BB_TAMANHO_ARQUIVO } caracteres");

                return null;
            }

            List<OrdemServico> ordens = new();

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
                Telefone = conteudo.Substring(76, 3),
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

            RegistrarChamadoClienteNoLog(osCliente);

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

        private void CriarArquivoRetornoAbertura(List<OrdemServico> chamados)
        {
            _logger.Info($"Iniciando a extração dos chamados nos arquivos");

            string fileName = "CRM549R.xPerto01." + DateTime.Now.ToString("ddMMyyyyHHMMsss");
            string target = Directory.GetCurrentDirectory() + "/Output";

            _logger.Info($"Lendo diretorio arquivos para processamento: { target }");

            using (StreamWriter w = new StreamWriter(target))
            {
                string cabecalho = MontarCabecalhoArquivoAbertura(chamados);
                
                w.WriteLine(cabecalho);
                _logger.Info($"Adicionado o cabecalho: { cabecalho }");

                chamados.ForEach(chamado =>
                {
                    string linha = MontarLinhaArquivo(chamado);

                    w.WriteLine(linha);

                    _logger.Info($"Adicionada a linha: { linha }");
                });
            }

            _logger.Info($"Finalizando a extração dos chamados nos arquivos");
        }

        private string MontarLinhaArquivo(OrdemServico chamado)
        {
            _logger.Info($"Iniciando a composição de nova linha no arquivo");
            
            string numOSCliente = chamado.NumOSCliente;
            string dataAbertura = chamado.DataHoraCad.Value.ToString("DDMMyyyy");
            string horaAbertura = chamado.DataHoraCad.Value.ToString("HHMM");
            string codOS = chamado.CodOS.ToString();
            
            _logger.Info($"Finalizando a composição de nova linha no arquivo");

            return $"3{ numOSCliente } { dataAbertura } { horaAbertura }000{ codOS }00";
        }
    
        private string MontarCabecalhoArquivoAbertura(List<OrdemServico> chamados) 
        {
            _logger.Info($"Iniciando a composição do cabeçalho do arquivo de abertura");

            string horaAtual = DateTime.Now.ToString("HHmms");
            string dataGeracao = DateTime.Now.ToString("DDMMyyy");
            string horaGeracao = DateTime.Now.ToString("HHmms");
            string qtdChamados = String.Format("{0:00000}", chamados.Count());

            _logger.Info($"Finalizando a composição do cabeçalho do arquivo de abertura");
            
            return $"2{ dataGeracao } { horaGeracao }3{ qtdChamados }crm549R       ";
        }

        private string MontarCabecalhoArquivoFechamento(List<OrdemServico> chamados) 
        {
            _logger.Info($"Iniciando a composição do cabeçalho do arquivo de fechamento");

            string horaAtual = DateTime.Now.ToString("HHmms");
            string dataGeracao = DateTime.Now.ToString("DDMMyyy");
            string horaGeracao = DateTime.Now.ToString("HHmms");
            string qtdChamados = String.Format("{0:00000}", chamados.Count());

            _logger.Info($"Iniciando a composição do cabeçalho do arquivo de fechamento");
            
            return $"2{ dataGeracao } { horaGeracao }3{ qtdChamados }crm549R       ";
        }

        private void CriarArquivoRetornoFechamento(List<OrdemServico> chamados)
        {
            _logger.Info($"Iniciando a criação dos arquivos de retorno");

            var parameters = new IntegracaoBBParameters {};
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
            foreach (PropertyInfo prop in chamado.GetType().GetProperties())
            {
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                if (type == typeof (DateTime))
                { 
                    _logger.Info($"Obtendo { type } do arquivo: { prop.GetValue(chamado, null).ToString() }");
                }
            }
        }
    }
}