using System;

namespace SAT.MODELS.Views
{
    public class ViewOrcamentoLista
    {
        public int? CodOrc  {get; set;}    
        public string Numero  {get; set;}             
        public DateTime? DataOrcamento  {get; set;}
        public int CodOS  {get; set;}     
        public DateTime DataAbertura {get; set;}       
        public int CodStatusServico {get; set;}                        
        public string StatusOS {get; set;}                        
        public string StatusLaudo {get; set;}
        public string NomeLocal {get; set;}                            
        public int CodTipoIntervencao {get; set;}               
        public string Intervencao {get; set;}               
        public string Autorizada {get; set;}
        public string Regiao {get; set;}
        public int CodCliente {get; set;}
        public string Cliente {get; set;}
        public string Equipamento {get; set;}
        public int CodFilial {get; set;}
        public string NumSerie {get; set;}
        public string NumOSCliente {get; set;}
        public decimal? ValorTotal { get; set; }
        public decimal? ValorTotalDesconto { get; set; }
    }
}

