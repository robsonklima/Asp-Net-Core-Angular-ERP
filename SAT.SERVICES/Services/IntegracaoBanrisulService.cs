using System;
using System.Linq;
using System.Threading.Tasks;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using SAT.UTILS;

namespace SAT.SERVICES.Services {
    public class IntegracaoBanrisulService : IIntegracaoBanrisulService
    {
        public struct Campo
        {
            public int Indice;
            public string Nome;
            public string Valor;
        }

        public Campo NumeroIncidente;
        public Campo DataHoraAbertura;
        public Campo NumeroSerie;
        public Campo CodigoLocalEquipamento;
        public Campo OrganizacaoFisicaEquipamento;
        public Campo NomeContato;
        public Campo TelefoneContato;
        public Campo HorarioAtendimento;
        public Campo ClassificacaoAtendimento;
        public Campo DescricaoIncidente;
        public Campo Rua;
        public Campo Cidade;
        public Campo Cep;
        public Campo StatusIncidente;
        public Campo PrioridadeIncidente;
        public Campo DataHoraAgendamento;
        public Campo DataHoraSolucaoValida;
        private readonly IEmailService _emailService;
        private readonly IOrdemServicoService _osService;

        public IntegracaoBanrisulService(
            IEmailService emailService,
            IOrdemServicoService osService
        )
        {
            _emailService = emailService;
            _osService = osService;

            NumeroIncidente = new Campo() { Indice = 0, Nome = "Numero Incidente" };
            DataHoraAbertura = new Campo() { Indice = 1, Nome = "Data Hora Abertura" };
            NumeroSerie = new Campo() { Indice = 2, Nome = "Numero Serie" };
            CodigoLocalEquipamento = new Campo() { Indice = 3, Nome = "Codigo Local Equipamento" };
            OrganizacaoFisicaEquipamento = new Campo() { Indice = 4, Nome = "Organizacao Fisica Equipamento" };
            NomeContato = new Campo() { Indice = 5, Nome = "Nome Contato" };
            TelefoneContato = new Campo() { Indice = 6, Nome = "Telefone Contato" };
            HorarioAtendimento = new Campo() { Indice = 7, Nome = "Horario Atendimento" };
            ClassificacaoAtendimento = new Campo() { Indice = 8, Nome = "Classificacao Atendimento" };
            DescricaoIncidente = new Campo() { Indice = 9, Nome = "Descricao Incidente" };
            Rua = new Campo() { Indice = 10, Nome = "Rua" };
            Cidade = new Campo() { Indice = 11, Nome = "Cidade" };
            Cep = new Campo() { Indice = 12, Nome = "Cep" };
            StatusIncidente = new Campo() { Indice = 13, Nome = "Status Incidente" };
            PrioridadeIncidente = new Campo() { Indice = 14, Nome = "Prioridade Incidente" };
            DataHoraAgendamento = new Campo() { Indice = 15, Nome = "Data Hora Agendamento" };
            DataHoraSolucaoValida = new Campo() { Indice = 16, Nome = "Data Hora Solucao Valida" };
        }

        public async Task ExecutarIntegracaoATMAsync()
        {
            var token = await _emailService.ObterTokenAsync();

            var emails = await _emailService.ObterEmailsAsync(token, Constants.EMAIL_TESTE_CONFIG.ClientID);

            foreach (var email in emails.Value)
            {
                string conteudo = StringHelper.GetStringBetweenCharacters(email.Body.Content, '#', '#');

                Carrega(conteudo);
            }
        }

        private void Carrega(string conteudo)
        {
            if (string.IsNullOrWhiteSpace(conteudo))
            {
                throw new Exception("Erro Integração Banrisul ATM: Não é possivel carregar o conteudo pois está vazio.");
            }

            conteudo = conteudo.Replace("||", "|");

            string[] dados = conteudo.Split('|');

            int quantidadeCampos = dados.Length == 17 ? 17 : 16;

            if (dados.Length != quantidadeCampos)
            {
                throw new Exception(@"Erro Integração Banrisul ATM: A quantidade de campos encontrados é diferente
                    do permitido. Conteudo encontrado: " + conteudo);
            }

            NumeroIncidente.Valor = dados[NumeroIncidente.Indice].ToString();
            DataHoraAbertura.Valor = dados[DataHoraAbertura.Indice].ToString();
            NumeroSerie.Valor = dados[NumeroSerie.Indice].ToString();
            CodigoLocalEquipamento.Valor = dados[CodigoLocalEquipamento.Indice].PadLeft(5, '0');
            OrganizacaoFisicaEquipamento.Valor = dados[OrganizacaoFisicaEquipamento.Indice].ToString();
            NomeContato.Valor = dados[NomeContato.Indice].ToString();
            TelefoneContato.Valor = dados[TelefoneContato.Indice].ToString();
            HorarioAtendimento.Valor = dados[HorarioAtendimento.Indice].ToString();
            ClassificacaoAtendimento.Valor = dados[ClassificacaoAtendimento.Indice].ToString();
            DescricaoIncidente.Valor = dados[DescricaoIncidente.Indice].ToString();
            Rua.Valor = dados[Rua.Indice].ToString();
            Cidade.Valor = dados[Cidade.Indice].ToString();
            Cep.Valor = dados[Cep.Indice].ToString();
            StatusIncidente.Valor = dados[StatusIncidente.Indice].ToString();
            PrioridadeIncidente.Valor = dados[PrioridadeIncidente.Indice].ToString();
            DataHoraAgendamento.Valor = dados[DataHoraAgendamento.Indice].ToString();

            if (StatusIncidente.Valor.ToUpper().Equals("RE"))
            {
                string dataHoraSolucao = !string.IsNullOrWhiteSpace(dados[DataHoraSolucaoValida.Indice].ToString())
                    ? dados[DataHoraSolucaoValida.Indice].ToString()
                    : dados[DataHoraSolucaoValida.Indice - 1].ToString();

                DataHoraSolucaoValida.Valor = dataHoraSolucao;
            }

            OrdemServico chamadoPerto = (OrdemServico)_osService.ObterPorParametros(new OrdemServicoParameters {
                NumOSCliente = NumeroIncidente.Valor,
                CodClientes = Constants.CLIENTE_BANRISUL.ToString()
            }).Items.FirstOrDefault();

            if (chamadoPerto != null) {
                
            } else {
                OrdemServico os = new OrdemServico {

                };


            }
        }
    }
}