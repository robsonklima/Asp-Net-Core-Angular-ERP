using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Chamado")]
    public partial class Chamado
    {
        public Chamado()
        {
            ChamadoDadosAdicionais = new HashSet<ChamadoDadosAdicionai>();
            ChamadoHists = new HashSet<ChamadoHist>();
            ChamadoOs = new HashSet<ChamadoO>();
        }

        [Key]
        public int CodChamado { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [Required]
        [Column("NumeroOSCliente")]
        [StringLength(10)]
        public string NumeroOscliente { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraAbertura { get; set; }
        [StringLength(200)]
        public string Classificacao { get; set; }
        [StringLength(20)]
        public string FoneOrigem { get; set; }
        [StringLength(15)]
        public string CnpjCpf { get; set; }
        [StringLength(200)]
        public string Nome { get; set; }
        [StringLength(200)]
        public string NomeFantasia { get; set; }
        [StringLength(200)]
        public string EnderecoSolicitante { get; set; }
        [StringLength(10)]
        public string Cep { get; set; }
        [StringLength(200)]
        public string Cidade { get; set; }
        [Column("UF")]
        [StringLength(2)]
        public string Uf { get; set; }
        [StringLength(100)]
        public string NomeContato { get; set; }
        [StringLength(50)]
        public string FoneContato { get; set; }
        [StringLength(11)]
        public string Rede { get; set; }
        [StringLength(15)]
        public string Estabelecimento { get; set; }
        [StringLength(8)]
        public string Terminal { get; set; }
        [StringLength(50)]
        public string Modelo { get; set; }
        [StringLength(50)]
        public string NumeroSerie { get; set; }
        [StringLength(3500)]
        public string Descricao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCadastro { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column("CodUsuarioOS")]
        [StringLength(20)]
        public string CodUsuarioOs { get; set; }
        [Column("DataHoraOS", TypeName = "datetime")]
        public DateTime? DataHoraOs { get; set; }
        [StringLength(20)]
        public string CodUsuarioAtendimento { get; set; }
        [StringLength(3500)]
        public string DescricaoAtendimento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HorarioInicioAtendimento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HorarioFimAtendimento { get; set; }
        public int? CodStatus { get; set; }
        public int? CodMotivoCancelamento { get; set; }
        [Column("CodDefeitoPOS")]
        public int? CodDefeitoPos { get; set; }
        public int? CodPosto { get; set; }
        public int? CodCliente { get; set; }
        public int? CodEquip { get; set; }
        public int? CodGrupoEquip { get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodEquipContrato { get; set; }
        [StringLength(8000)]
        public string Linha { get; set; }
        [StringLength(50)]
        public string ComplementoDefeito { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataSolicitacao { get; set; }
        public bool? ExigeVisitaTecnica { get; set; }
        public int? CodOperadoraTelefonia { get; set; }
        [StringLength(10)]
        public string Versao { get; set; }

        [ForeignKey("CodEquip,CodGrupoEquip,CodTipoEquip")]
        [InverseProperty(nameof(Equipamento.Chamados))]
        public virtual Equipamento Cod { get; set; }
        [ForeignKey(nameof(CodDefeitoPos))]
        [InverseProperty(nameof(DefeitoPo.Chamados))]
        public virtual DefeitoPo CodDefeitoPosNavigation { get; set; }
        [ForeignKey(nameof(CodEquipContrato))]
        [InverseProperty(nameof(EquipamentoContrato.Chamados))]
        public virtual EquipamentoContrato CodEquipContratoNavigation { get; set; }
        [ForeignKey(nameof(CodMotivoCancelamento))]
        [InverseProperty(nameof(MotivoCancelamento.Chamados))]
        public virtual MotivoCancelamento CodMotivoCancelamentoNavigation { get; set; }
        [ForeignKey(nameof(CodOperadoraTelefonia))]
        [InverseProperty(nameof(OperadoraTelefonium.Chamados))]
        public virtual OperadoraTelefonium CodOperadoraTelefoniaNavigation { get; set; }
        [ForeignKey(nameof(CodOs))]
        [InverseProperty(nameof(O.Chamados))]
        public virtual O CodOsNavigation { get; set; }
        [ForeignKey(nameof(CodStatus))]
        [InverseProperty(nameof(StatusChamado.Chamados))]
        public virtual StatusChamado CodStatusNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioAtendimento))]
        [InverseProperty(nameof(Usuario.ChamadoCodUsuarioAtendimentoNavigations))]
        public virtual Usuario CodUsuarioAtendimentoNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCadastro))]
        [InverseProperty(nameof(Usuario.ChamadoCodUsuarioCadastroNavigations))]
        public virtual Usuario CodUsuarioCadastroNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioOs))]
        [InverseProperty(nameof(Usuario.ChamadoCodUsuarioOsNavigations))]
        public virtual Usuario CodUsuarioOsNavigation { get; set; }
        [InverseProperty(nameof(ChamadoDadosAdicionai.CodChamadoNavigation))]
        public virtual ICollection<ChamadoDadosAdicionai> ChamadoDadosAdicionais { get; set; }
        [InverseProperty(nameof(ChamadoHist.CodChamadoNavigation))]
        public virtual ICollection<ChamadoHist> ChamadoHists { get; set; }
        [InverseProperty(nameof(ChamadoO.CodChamadoNavigation))]
        public virtual ICollection<ChamadoO> ChamadoOs { get; set; }
    }
}
