using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class ChamadoDadosAdicionais
    {
        public int CodChamadoDadosAdicionais { get; set; }
        [Key]
        public int CodChamado { get; set; }
        public string SimCard { get; set; }
        public string DDD { get; set; }
        public string Numero { get; set; }
        public int CodOperadoraTelefonia { get; set; }
        public string OperadoraTelefoniaTela { get; set; }
        public DateTime? Autenticacao { get; set; }
        public string SimCardHist { get; set; }
        public string DDDHist { get; set; }
        public string NumeroHist { get; set; }
        public int CodOperadoraTelefoniaHist { get; set; }
        public string OperadoraTelefoniaTelaHist { get; set; }
        public DateTime AutenticacaoHist { get; set; }
        public int CodTipoComunicacao { get; set; }
        public string TipoComunicacaoTela { get; set; }
        public string CodUsuarioCadastro { get; set; }
        public string ModoEquipamento { get; set; }
    }
}