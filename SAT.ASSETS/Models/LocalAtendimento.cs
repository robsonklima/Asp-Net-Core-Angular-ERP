using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("LocalAtendimento")]
    [Index(nameof(CodPosto), Name = "uk_localatendimento", IsUnique = true)]
    public partial class LocalAtendimento
    {
        [Key]
        public int CodPosto { get; set; }
        [Key]
        public int CodCliente { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeLocal { get; set; }
        [StringLength(6)]
        public string NumAgencia { get; set; }
        [Column("DCPosto")]
        [StringLength(4)]
        public string Dcposto { get; set; }
        public int CodTipoRota { get; set; }
        [Column("CNPJ")]
        [StringLength(20)]
        public string Cnpj { get; set; }
        [StringLength(20)]
        public string InscricaoEstadual { get; set; }
        [Column("CEP")]
        [StringLength(8)]
        public string Cep { get; set; }
        [StringLength(250)]
        public string Endereco { get; set; }
        [StringLength(200)]
        public string EnderecoComplemento { get; set; }
        [StringLength(100)]
        public string Bairro { get; set; }
        public int CodCidade { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Site { get; set; }
        [StringLength(50)]
        public string Fone { get; set; }
        [StringLength(20)]
        public string Fax { get; set; }
        [StringLength(50)]
        public string DescTurno { get; set; }
        [StringLength(50)]
        public string Longitude { get; set; }
        [StringLength(50)]
        public string Latitude { get; set; }
        [StringLength(250)]
        public string EnderecoCoordenadas { get; set; }
        [StringLength(60)]
        public string BairroCoordenadas { get; set; }
        [StringLength(60)]
        public string CidadeCoordenadas { get; set; }
        [Column("UFCoordenadas")]
        [StringLength(2)]
        public string Ufcoordenadas { get; set; }
        [StringLength(100)]
        public string PaisCoordenadas { get; set; }
        [Column("DistanciaKmPAT_Res", TypeName = "decimal(10, 2)")]
        public decimal? DistanciaKmPatRes { get; set; }
        [StringLength(300)]
        public string Observacao { get; set; }
        public byte IndAtivo { get; set; }
        [Column("IndPAE_DEL")]
        public byte? IndPaeDel { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [Column("Origem_DEL")]
        [StringLength(20)]
        public string OrigemDel { get; set; }
        [Column("NumAgenciaCliente_DEL")]
        [StringLength(5)]
        public string NumAgenciaClienteDel { get; set; }
        [Column("DCPostoCliente_DEL")]
        [StringLength(2)]
        public string DcpostoClienteDel { get; set; }
        [Column("NumAgencia2_DEL")]
        [StringLength(10)]
        public string NumAgencia2Del { get; set; }
        [Column("Telefone1_DEL")]
        [StringLength(20)]
        public string Telefone1Del { get; set; }
        [Column("Telefone2_DEL")]
        [StringLength(20)]
        public string Telefone2Del { get; set; }
        [StringLength(20)]
        public string NumeroEnd { get; set; }
        [Column("ComplemEnd_DEL")]
        [StringLength(20)]
        public string ComplemEndDel { get; set; }
        [Column("NumeroEndEntrega_DEL")]
        [StringLength(20)]
        public string NumeroEndEntregaDel { get; set; }
        [Column("ComplemEndEntrega_DEL")]
        [StringLength(20)]
        public string ComplemEndEntregaDel { get; set; }
        [Column("EnderecoEntrega_DEL")]
        [StringLength(100)]
        public string EnderecoEntregaDel { get; set; }
        [Column("BairroEntrega_DEL")]
        [StringLength(20)]
        public string BairroEntregaDel { get; set; }
        [Column("CidadeEntrega_DEL")]
        [StringLength(30)]
        public string CidadeEntregaDel { get; set; }
        [Column("SiglaUFEntrega_DEL")]
        [StringLength(2)]
        public string SiglaUfentregaDel { get; set; }
        [Column("CEPEntrega_DEL")]
        [StringLength(20)]
        public string CepentregaDel { get; set; }
        [Column("TelefoneEntrega_DEL")]
        [StringLength(20)]
        public string TelefoneEntregaDel { get; set; }
        [Column("FaxEntrega_DEL")]
        [StringLength(20)]
        public string FaxEntregaDel { get; set; }
        [Column("ObsCliente_DEL")]
        [StringLength(1000)]
        public string ObsClienteDel { get; set; }
        [Column("DataCadastro_DEL", TypeName = "datetime")]
        public DateTime? DataCadastroDel { get; set; }
        [Column("CodUsuarioCadastro_DEL")]
        [StringLength(20)]
        public string CodUsuarioCadastroDel { get; set; }
        [Column("DataManutencao_DEL", TypeName = "datetime")]
        public DateTime? DataManutencaoDel { get; set; }
        [Column("CodUsuarioManutencao_DEL")]
        [StringLength(20)]
        public string CodUsuarioManutencaoDel { get; set; }
        public int? CodRegiao { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodFilial { get; set; }
        [StringLength(30)]
        public string Cidade { get; set; }
        [Column("SiglaUF")]
        [StringLength(2)]
        public string SiglaUf { get; set; }
        public int? CodRegional { get; set; }
        [Column("CNPJFaturamento")]
        [StringLength(20)]
        public string Cnpjfaturamento { get; set; }
        [StringLength(2000)]
        public string SenhaAcessoNotaFiscal { get; set; }
    }
}
