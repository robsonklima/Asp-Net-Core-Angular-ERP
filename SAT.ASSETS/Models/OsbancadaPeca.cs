using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OSBancadaPecas")]
    public partial class OsbancadaPeca
    {
        public OsbancadaPeca()
        {
            DataHoraTrabs = new HashSet<DataHoraTrab>();
            OsbancadaPecasAplics = new HashSet<OsbancadaPecasAplic>();
            OsbancadaPecasOrcamentos = new HashSet<OsbancadaPecasOrcamento>();
        }

        [Key]
        [Column("CodOSBancada")]
        public int CodOsbancada { get; set; }
        [Key]
        [Column("CodPecaRE5114")]
        public int CodPecaRe5114 { get; set; }
        [Column("CodFilialRE5114")]
        public int? CodFilialRe5114 { get; set; }
        public byte? IndGarantia { get; set; }
        [StringLength(1000)]
        public string DefeitoRelatado { get; set; }
        [StringLength(1000)]
        public string DefeitoConstatado { get; set; }
        [StringLength(1000)]
        public string Solucao { get; set; }
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [Column("MotivoQGarantia")]
        [StringLength(1000)]
        public string MotivoQgarantia { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataManut { get; set; }
        public byte? IndPecaLiberada { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraPecaLiberada { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorEntrada { get; set; }
        public byte? IndPecaDevolvida { get; set; }
        [Column("CodPecaRE5114Troca")]
        public int? CodPecaRe5114troca { get; set; }
        [Column("NumItemNF")]
        public int? NumItemNf { get; set; }
        [StringLength(50)]
        public string NomeTecnicoRelatante { get; set; }

        [ForeignKey(nameof(CodOsbancada))]
        [InverseProperty(nameof(Osbancadum.OsbancadaPecas))]
        public virtual Osbancadum CodOsbancadaNavigation { get; set; }
        [ForeignKey(nameof(CodPecaRe5114))]
        [InverseProperty(nameof(PecaRe5114.OsbancadaPecas))]
        public virtual PecaRe5114 CodPecaRe5114Navigation { get; set; }
        [InverseProperty(nameof(DataHoraTrab.Cod))]
        public virtual ICollection<DataHoraTrab> DataHoraTrabs { get; set; }
        [InverseProperty(nameof(OsbancadaPecasAplic.Cod))]
        public virtual ICollection<OsbancadaPecasAplic> OsbancadaPecasAplics { get; set; }
        [InverseProperty(nameof(OsbancadaPecasOrcamento.Cod))]
        public virtual ICollection<OsbancadaPecasOrcamento> OsbancadaPecasOrcamentos { get; set; }
    }
}
