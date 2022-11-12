namespace SAT.MODELS.Views
{
    public class AuditoriaView
    {
       public int CodAuditoria { get; set; }
       public string CodUsuario { get; set; }
       public byte? IndAtivo { get; set; }
       public string NomeUsuario { get; set; }
       public byte CodAuditoriaStatus { get; set; }
       public string NomeAuditoriaStatus { get; set; }
       public string NumeroCartao { get; set; }
       public int CodFilial { get; set; }
       public string NomeFilial { get; set; }
       public int? QtdDiasAuditoriaAnterior { get; set; }
       public int? QtdDespesasPendentes { get; set; }
       public int? OdometroAnterior { get; set; }
       public int? OdometroAtual { get; set; }
       public double? QuilometrosPorLitro { get; set; }
    }
}