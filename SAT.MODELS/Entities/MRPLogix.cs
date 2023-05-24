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
        public decimal? QtdPedido { get; set; }
        public decimal? QtdSolicitada { get; set; }
        public string LocalProd { get; set; }        
        public decimal? QtdCancelada { get; set; }
        public decimal? Preco { get; set; }
        public decimal? QtdAtendida { get; set; }
        public DateTime? PrazoEntrega { get; set; }            
        public int? DiasPedido { get; set; }
        public int? CodEmpresa { get; set; }
        public string LocalEstoque { get; set; }
        public decimal? NumSequencia { get; set; }
        public decimal? IPI { get; set; }
        public string CodUsuario { get; set; }
        public string Tipo { get; set; }
        public decimal? SaldoTotal { get; set; }
        public decimal? NumSequenciaPed { get; set; }
        public decimal? Saldo { get; set; }                
    }
}
