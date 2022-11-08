using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities {
    public class ORItem
    {
        public int CodORItem { get; set; }
        public DateTime DataHoraORItem { get; set; }
        public int CodOR { get; set; }
        public int CodPeca { get; set; }
        public Peca Peca { get; set; }
        public int CodStatus { get; set; }
        public int Quantidade { get; set; }
        public string NumSerie { get; set; }
        public int? CodTipoOR { get; set; }
        public ORTipo ORTipo { get; set; }
        public int? CodOS { get; set; }
        public OrdemServico OrdemServico { get; set; }
        public int? CodCliente { get; set; }
        public Cliente Cliente { get; set; }
        public string CodTecnico { get; set; }
        public Usuario UsuarioTecnico { get; set; }
        public string DefeitoRelatado { get; set; }
        public string RelatoSolucao { get; set; }
        public int? CodDefeito { get; set; }
        public int? CodAcao { get; set; }
        public int? CodSolucao { get; set; }
        public byte? IndConfLog { get; set; }
        public byte? IndConfLab { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public Usuario UsuarioCadastro { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string DivergenciaDescricao { get; set; }
        public DateTime? DataConfLab { get; set; }
        public DateTime? DataConfLog { get; set; }
        public int? CodStatusOR { get; set; }
        public ORStatus StatusOR { get; set; }
        public int? IndPrioridade { get; set; }
        public int? DiasEmReparo { get; set; }
        public List<ORTempoReparo> TemposReparo { get; set; }
        public ORDefeito ORDefeito { get; set; }
    }
}