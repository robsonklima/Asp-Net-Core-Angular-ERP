namespace SAT.MODELS.ViewModels
{
    public class OrdemServicoExcelViewModel
    {
        public int Chamado { get; set; }
        public string NumOSCliente { get; set; }
        public string DataAbertura { get; set; }
        public string DataSolicitacao { get; set; }
        public string LimiteAtendimento { get; set; }
        public string Status { get; set; }
        public string Intervencao { get; set; }
        public string Tecnico { get; set; }
        public string NumBanco { get; set; }
        public string NumAgencia { get; set; }
        public string Local { get; set; }
        public string Equipamento { get; set; }
        public string Serie { get; set; }
        public string SLA { get; set; }
        public int? PA { get; set; }
        public string Regiao { get; set; }
        public string Defeito { get; set; }
        public string Autorizada { get; set; }
        public int? Reincidencia { get; set; }
    }
}