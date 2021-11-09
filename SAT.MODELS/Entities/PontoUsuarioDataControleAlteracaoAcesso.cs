using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities {
    public class PontoUsuarioDataControleAlteracaoAcesso
    {
        [Key]
        public int CodPontoUsuarioDataControleAlteracaoAcesso { get; set; }
        public int CodPontoUsuarioData { get; set; }
        public int CodPontoUsuarioDataModoAlteracaoAcesso { get; set; }
        public int CodPontoUsuarioDataStatusAcesso { get; set; }
        public int CodPontoUsuarioDataJustificativaAlteracaoAcesso { get; set; }
        public string CodUsuario { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}