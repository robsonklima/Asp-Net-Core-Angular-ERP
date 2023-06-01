using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Orcamento")]
    public partial class Orcamento
    {
        public Orcamento()
        {
            OrcamentoNfs = new HashSet<OrcamentoNf>();
        }

        [Key]
        public int CodOrcamento { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [StringLength(100)]
        public string Endereco { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataOrcamento { get; set; }
        [StringLength(20)]
        public string Bairro { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValOrcamento { get; set; }
        [StringLength(20)]
        public string NumeroEnd { get; set; }
        public byte? IndAprovado { get; set; }
        [StringLength(20)]
        public string ComplemEnd { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAprovacao { get; set; }
        [StringLength(50)]
        public string NomeAprovador { get; set; }
        public int? CodCidade { get; set; }
        [StringLength(100)]
        public string NomeResponsavel { get; set; }
        [StringLength(200)]
        public string DescMotivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataValidade { get; set; }
        public int? PrazoExecucao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraExecucao { get; set; }
        [Column("CodUF")]
        public int? CodUf { get; set; }
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraUsuarioCadastro { get; set; }
        [StringLength(100)]
        public string MotivoRep { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManutencao { get; set; }

        [InverseProperty(nameof(OrcamentoNf.CodOrcamentoNavigation))]
        public virtual ICollection<OrcamentoNf> OrcamentoNfs { get; set; }
    }
}
