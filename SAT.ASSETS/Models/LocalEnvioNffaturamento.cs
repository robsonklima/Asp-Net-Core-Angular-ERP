using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("LocalEnvioNFFaturamento")]
    public partial class LocalEnvioNffaturamento
    {
        public LocalEnvioNffaturamento()
        {
            LocalEnvioNffaturamentoVinculados = new HashSet<LocalEnvioNffaturamentoVinculado>();
        }

        [Key]
        [Column("CodLocalEnvioNFFaturamento")]
        public int CodLocalEnvioNffaturamento { get; set; }
        public int CodCliente { get; set; }
        public int CodContrato { get; set; }
        [StringLength(100)]
        public string RazaoSocialFaturamento { get; set; }
        [StringLength(150)]
        public string EnderecoFaturamento { get; set; }
        [StringLength(80)]
        public string ComplementoFaturamento { get; set; }
        [StringLength(20)]
        public string NumeroFaturamento { get; set; }
        [StringLength(60)]
        public string BairroFaturamento { get; set; }
        [Column("CNPJFaturamento")]
        [StringLength(18)]
        public string Cnpjfaturamento { get; set; }
        [StringLength(15)]
        public string InscricaoEstadualFaturamento { get; set; }
        [StringLength(60)]
        public string ResponsavelFaturamento { get; set; }
        [StringLength(150)]
        public string EmailFaturamento { get; set; }
        [StringLength(15)]
        public string FoneFaturamento { get; set; }
        [StringLength(15)]
        public string FaxFaturamento { get; set; }
        public byte? IndAtivoFaturamento { get; set; }
        [Column("CEPFaturamento")]
        [StringLength(50)]
        public string Cepfaturamento { get; set; }
        [Column("CodUFFaturamento")]
        public int? CodUffaturamento { get; set; }
        public int? CodCidadeFaturamento { get; set; }
        [Column("RazaoSocialEnvioNF")]
        [StringLength(1000)]
        public string RazaoSocialEnvioNf { get; set; }
        [Column("EnderecoEnvioNF")]
        [StringLength(150)]
        public string EnderecoEnvioNf { get; set; }
        [Column("ComplementoEnvioNF")]
        [StringLength(80)]
        public string ComplementoEnvioNf { get; set; }
        [Column("NumeroEnvioNF")]
        [StringLength(20)]
        public string NumeroEnvioNf { get; set; }
        [Column("BairroEnvioNF")]
        [StringLength(60)]
        public string BairroEnvioNf { get; set; }
        [Column("CNPJEnvioNF")]
        [StringLength(18)]
        public string CnpjenvioNf { get; set; }
        [Column("InscricaoEstadualEnvioNF")]
        [StringLength(15)]
        public string InscricaoEstadualEnvioNf { get; set; }
        [Column("ResponsavelEnvioNF")]
        [StringLength(60)]
        public string ResponsavelEnvioNf { get; set; }
        [Column("EmailEnvioNF")]
        [StringLength(150)]
        public string EmailEnvioNf { get; set; }
        [Column("FoneEnvioNF")]
        [StringLength(15)]
        public string FoneEnvioNf { get; set; }
        [Column("FaxEnvioNF")]
        [StringLength(15)]
        public string FaxEnvioNf { get; set; }
        [Column("IndAtivoEnvioNF")]
        public byte? IndAtivoEnvioNf { get; set; }
        [Column("CEPEnvioNF")]
        [StringLength(50)]
        public string CepenvioNf { get; set; }
        [Column("CodCidadeEnvioNF")]
        public int? CodCidadeEnvioNf { get; set; }
        [Column("CodUFEnvioNF")]
        public int? CodUfenvioNf { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodCidadeEnvioNf))]
        [InverseProperty(nameof(Cidade.LocalEnvioNffaturamentoCodCidadeEnvioNfNavigations))]
        public virtual Cidade CodCidadeEnvioNfNavigation { get; set; }
        [ForeignKey(nameof(CodCidadeFaturamento))]
        [InverseProperty(nameof(Cidade.LocalEnvioNffaturamentoCodCidadeFaturamentoNavigations))]
        public virtual Cidade CodCidadeFaturamentoNavigation { get; set; }
        [ForeignKey(nameof(CodCliente))]
        [InverseProperty(nameof(Cliente.LocalEnvioNffaturamentos))]
        public virtual Cliente CodClienteNavigation { get; set; }
        [ForeignKey(nameof(CodContrato))]
        [InverseProperty(nameof(Contrato.LocalEnvioNffaturamentos))]
        public virtual Contrato CodContratoNavigation { get; set; }
        [ForeignKey(nameof(CodUfenvioNf))]
        [InverseProperty(nameof(Uf.LocalEnvioNffaturamentoCodUfenvioNfNavigations))]
        public virtual Uf CodUfenvioNfNavigation { get; set; }
        [ForeignKey(nameof(CodUffaturamento))]
        [InverseProperty(nameof(Uf.LocalEnvioNffaturamentoCodUffaturamentoNavigations))]
        public virtual Uf CodUffaturamentoNavigation { get; set; }
        [InverseProperty(nameof(LocalEnvioNffaturamentoVinculado.CodLocalEnvioNffaturamentoNavigation))]
        public virtual ICollection<LocalEnvioNffaturamentoVinculado> LocalEnvioNffaturamentoVinculados { get; set; }
    }
}
