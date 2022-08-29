namespace SAT.MODELS.Entities
{
    public class IntegracaoBanrisulAtendimento
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

        public IntegracaoBanrisulAtendimento()
        {
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
    }
}