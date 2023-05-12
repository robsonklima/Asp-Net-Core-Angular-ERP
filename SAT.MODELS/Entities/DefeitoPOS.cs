using System;

namespace SAT.MODELS.Entities
{
    public class DefeitoPOS
    {
        public int CodDefeitoPOS { get; set; }
        public string NomeDefeitoPOS { get; set; }
        public DateTime DataCadastro { get; set; }
        public string CodUsuarioCadastro { get; set; }
        public bool Ativo { get; set; }
        public int CodDefeito { get; set; }
        public bool ExigeTrocaEquipamento { get; set; }
    }
}