using System;

namespace SAT.MODELS.Entities
{
    public class MRPLogix
    {
        public int CodMRPLogix { get; set; }
        public string NumPedido { get; set; }
        public DateTime? DataPedido { get; set; }
        public string Nomecliente { get; set; }
        public string CodItem { get; set; }
        public string NomeItem { get; set; }
        public string CodCliente { get; set; }
        public double? QtdPedido { get; set; }
        public double? QtdSolicitada { get; set; }
        public string LocalProd { get; set; }        
        public double? QtdCancelada { get; set; }
        public double? Preco { get; set; }
        public double? QtdAtendida { get; set; }
        public DateTime? PrazoEntrega { get; set; }            
        public int? DiasPedido { get; set; }
        public int? CodEmpresa { get; set; }
        public string LocalEstoque { get; set; }
        public double? NumSequencia { get; set; }
        public double? IPI { get; set; }
        public string CodUsuario { get; set; }
        public string Tipo { get; set; }
        public double? SaldoTotal { get; set; }
        public double? NumSequenciaPed { get; set; }
        public double? Saldo { get; set; }                
    }
}
