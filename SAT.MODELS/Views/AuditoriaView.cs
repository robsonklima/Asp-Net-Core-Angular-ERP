namespace SAT.MODELS.Views
{
    public class AuditoriaView
    {
       public int CodAuditoria { get; set; }
       public string CodUsuario { get; set; }
       public string NomeUsuario { get; set; }
       public byte CodAuditoriaStatus { get; set; }
       public string NomeAuditoriaStatus { get; set; }
       public int CodAuditoriaVeiculo { get; set; }
       public string Placa { get; set; }
       public string NumeroCartao { get; set; }
       public int CodAuditoriaVeiculoTanque { get; set; }
       public string NomeAuditoriaVeiculoTanque { get; set; }
       public int CodFilial { get; set; }
       public string NomeFilial { get; set; }
       public int? QtdDiasAuditoriaAnterior { get; set; }
       public int? QtdDespesasPendentes { get; set; }
       public int? OdometroAnterior { get; set; }
       public int? OdometroAtual { get; set; }
    }
}