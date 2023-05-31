using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcOsNew
    {
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [Column("DataHoraAberturaOS", TypeName = "datetime")]
        public DateTime? DataHoraAberturaOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraSolicitacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraAgendamento { get; set; }
        public int? CodEquipContrato { get; set; }
        [StringLength(50)]
        public string NomeEquip { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeCliente { get; set; }
        [Required]
        [StringLength(3)]
        public string NumBanco { get; set; }
        [Required]
        [StringLength(5)]
        public string NumAgencia { get; set; }
        [Required]
        [Column("DCPosto")]
        [StringLength(2)]
        public string Dcposto { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeLocal { get; set; }
        [StringLength(150)]
        public string Endereco { get; set; }
        [StringLength(100)]
        public string Bairro { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeCidade { get; set; }
        [Required]
        [Column("SiglaUF")]
        [StringLength(50)]
        public string SiglaUf { get; set; }
        [StringLength(50)]
        public string DescTurno { get; set; }
        [StringLength(50)]
        public string NomeSolicitante { get; set; }
        public int CodTipoIntervencao { get; set; }
        public int CodStatusServico { get; set; }
        [StringLength(3500)]
        public string DefeitoRelatado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataManutencao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraFimSla { get; set; }
        public int IndBloqueioReincidencia { get; set; }
        [Column("EnderecoOS")]
        [StringLength(8000)]
        public string EnderecoOs { get; set; }
        [StringLength(5)]
        public string Status { get; set; }
        [Column("CodETipoIntervencao")]
        [StringLength(5)]
        public string CodEtipoIntervencao { get; set; }
        [Required]
        [Column("PATRegiao")]
        [StringLength(103)]
        public string Patregiao { get; set; }
        [Column("PATRegiaoAbrev")]
        [StringLength(57)]
        public string PatregiaoAbrev { get; set; }
        [Column("NomeSLA")]
        [StringLength(50)]
        public string NomeSla { get; set; }
        public int? CodTipoEquip { get; set; }
        [Required]
        [StringLength(3)]
        public string PontoEstrategico { get; set; }
        public int? NumReincidencia { get; set; }
    }
}
