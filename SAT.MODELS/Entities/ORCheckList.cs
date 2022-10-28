using System;

namespace SAT.MODELS.Entities
{
    public class ORCheckList
    {
        public int CodORCheckList { get; set; }
        public string Descricao { get; set; }
        public string CodMagnus { get; set; }
        public int CodPeca { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public Usuario UsuarioCadastro { get; set; }
        public DateTime DataHoraCad { get; set; }
        public int? CodOritem { get; set; }
        public string CodORCheckListItem { get; set; }
        public int? TempoReparo { get; set; }
    }
}