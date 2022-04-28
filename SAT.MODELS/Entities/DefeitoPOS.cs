using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class DefeitoPOS
    {
        [Key]
        public int CodDefeitoPOS { get; set; }
        [Column("DefeitoPOS")]
        public string NomeDefeitoPOS { get; set; }
        public DateTime DataCadastro { get; set; }
        public string CodUsuarioCadastro { get; set; }
        public bool Ativo { get; set; }
        public int CodDefeito { get; set; }
        public bool ExigeTrocaEquipamento { get; set; }
    }
}