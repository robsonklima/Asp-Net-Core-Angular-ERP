using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class RecuperaSenha
    {
        [Key]
        public int CodRecuperaSenha { get; set; }
        public string CodUsuario { get; set; }
        public string Email { get; set; }
        public DateTime DataHoraCad { get; set; }
        public byte SolicitacaoConfirmada { get; set; }
        public byte IndAtivo { get; set; }
    }
}
