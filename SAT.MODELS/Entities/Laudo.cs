using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class Laudo
    {
        public int CodLaudo { get; set; }
        public int CodOS { get; set; }
        public int CodRAT { get; set; }
        public int CodTecnico { get; set; }
        public string RelatoCliente { get; set; }
        public string Conclusao { get; set; }
        public DateTime DataHoraCad { get; set; }
        public int CodLaudoStatus { get; set; }
        public string NomeCliente { get; set; }
        public string MatriculaCliente { get; set; }
        public string TensaoComCarga { get; set; }
        public string TensaoSemCarga { get; set; }
        public string TensaoTerraENeutro { get; set; }
        public string Temperatura { get; set; }
        public string Justificativa { get; set; }
        public int? IndRedeEstabilizada { get; set; }
        public int? IndPossuiNobreak { get; set; }
        public int? IndPossuiArCond { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public int? IndAtivo { get; set; }
        public List<LaudoSituacao> LaudosSituacao { get; set; }
        public LaudoStatus LaudoStatus { get; set; }
        public OrdemServico Or { get; set; }
        public Tecnico Tecnico { get; set; }
        public RelatorioAtendimento Rat { get; set;}
    }
}