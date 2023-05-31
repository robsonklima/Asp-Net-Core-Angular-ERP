using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class ChamadoDadosAdicionai
    {
        [Key]
        public int CodChamadoDadosAdicionais { get; set; }
        public int CodChamado { get; set; }
        [StringLength(50)]
        public string SimCard { get; set; }
        [Column("DDD")]
        [StringLength(10)]
        public string Ddd { get; set; }
        [StringLength(50)]
        public string Numero { get; set; }
        public int? CodOperadoraTelefonia { get; set; }
        [StringLength(50)]
        public string OperadoraTelefoniaTela { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Autenticacao { get; set; }
        [StringLength(50)]
        public string SimCardHist { get; set; }
        [Column("DDDHist")]
        [StringLength(10)]
        public string Dddhist { get; set; }
        [StringLength(50)]
        public string NumeroHist { get; set; }
        public int? CodOperadoraTelefoniaHist { get; set; }
        [StringLength(50)]
        public string OperadoraTelefoniaTelaHist { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AutenticacaoHist { get; set; }
        public int? CodTipoComunicacao { get; set; }
        [StringLength(50)]
        public string TipoComunicacaoTela { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [StringLength(50)]
        public string ModoEquipamento { get; set; }

        [ForeignKey(nameof(CodChamado))]
        [InverseProperty(nameof(Chamado.ChamadoDadosAdicionais))]
        public virtual Chamado CodChamadoNavigation { get; set; }
        [ForeignKey(nameof(CodOperadoraTelefoniaHist))]
        [InverseProperty(nameof(OperadoraTelefonium.ChamadoDadosAdicionaiCodOperadoraTelefoniaHistNavigations))]
        public virtual OperadoraTelefonium CodOperadoraTelefoniaHistNavigation { get; set; }
        [ForeignKey(nameof(CodOperadoraTelefonia))]
        [InverseProperty(nameof(OperadoraTelefonium.ChamadoDadosAdicionaiCodOperadoraTelefoniaNavigations))]
        public virtual OperadoraTelefonium CodOperadoraTelefoniaNavigation { get; set; }
        [ForeignKey(nameof(CodTipoComunicacao))]
        [InverseProperty(nameof(TipoComunicacao.ChamadoDadosAdicionais))]
        public virtual TipoComunicacao CodTipoComunicacaoNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCadastro))]
        [InverseProperty(nameof(Usuario.ChamadoDadosAdicionais))]
        public virtual Usuario CodUsuarioCadastroNavigation { get; set; }
    }
}
