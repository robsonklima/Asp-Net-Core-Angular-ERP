using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwRelatorioGenerico
    {
        [Required]
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeTipoContrato { get; set; }
        public int CodContrato { get; set; }
        public int? CodContratoPai { get; set; }
        public int? CodCliente { get; set; }
        public int? CodTipoContrato { get; set; }
        [Column("CNPJ")]
        [StringLength(20)]
        public string Cnpj { get; set; }
        [StringLength(50)]
        public string NroContrato { get; set; }
        [StringLength(100)]
        public string NomeContrato { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataContrato { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAssinatura { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataInicioVigencia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFimVigencia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataInicioPeriodoReajuste { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFimPeriodoReajuste { get; set; }
        [StringLength(50)]
        public string NomeResponsavelPerto { get; set; }
        [StringLength(50)]
        public string NomeResponsavelCliente { get; set; }
        [StringLength(2000)]
        public string ObjetoContrato { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValTotalContrato { get; set; }
        public short? NumMinReincidencia { get; set; }
        public int? KmMinimoAdicional { get; set; }
        public int? KmAdicionalHora { get; set; }
        [Column("MTBFNominal")]
        public int Mtbfnominal { get; set; }
        public byte? IndAtivo { get; set; }
        public byte? IndPermitePecaGenerica { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataManutencao { get; set; }
        [StringLength(20)]
        public string CodUsuarioManutencao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HoraDispInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HoraDispFim { get; set; }
        public byte? IndDispSabado { get; set; }
        public byte? IndDispDomingo { get; set; }
        public byte? IndDisp24H { get; set; }
        [Column("CodSLA")]
        public int? CodSla { get; set; }
        public short? NumDiasSubstEquip { get; set; }
        public int? CodEmpresa { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataReajuste { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCancelamento { get; set; }
        [StringLength(20)]
        public string CodUsuarioCancelamento { get; set; }
        [StringLength(2000)]
        public string MotivoCancelamento { get; set; }
        [StringLength(20)]
        public string NumeroEnd { get; set; }
        [StringLength(20)]
        public string ComplemEnd { get; set; }
        [StringLength(100)]
        public string EnderecoCobranca { get; set; }
        [StringLength(20)]
        public string BairroCobranca { get; set; }
        [StringLength(20)]
        public string CidadeCobranca { get; set; }
        [Column("SiglaUFCobranca")]
        [StringLength(20)]
        public string SiglaUfcobranca { get; set; }
        [Column("CEPCobranca")]
        [StringLength(20)]
        public string Cepcobranca { get; set; }
        [StringLength(20)]
        public string TelefoneCobranca { get; set; }
        [Column("FAXCobranca")]
        [StringLength(20)]
        public string Faxcobranca { get; set; }
        public byte? IndGarantia { get; set; }
        public byte? IndHerenca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? PercReajuste { get; set; }
        public byte? IndPermitePecaEspecifica { get; set; }
        public string SemCobertura { get; set; }
    }
}
