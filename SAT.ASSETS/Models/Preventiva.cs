using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Preventiva")]
    public partial class Preventiva
    {
        [Key]
        public int CodPreventiva { get; set; }
        public int CodPreventivaImportacao { get; set; }
        [Required]
        [StringLength(50)]
        public string Tipo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [StringLength(50)]
        public string Id { get; set; }
        [StringLength(150)]
        public string Cliente { get; set; }
        [StringLength(50)]
        public string Filial { get; set; }
        [StringLength(250)]
        public string Regiao { get; set; }
        [StringLength(50)]
        public string Agencia { get; set; }
        [Column("DC")]
        [StringLength(50)]
        public string Dc { get; set; }
        [StringLength(250)]
        public string Local { get; set; }
        [StringLength(2000)]
        public string Endereco { get; set; }
        [StringLength(500)]
        public string Cidade { get; set; }
        [Column("UF")]
        [StringLength(50)]
        public string Uf { get; set; }
        [StringLength(50)]
        public string Serie { get; set; }
        [StringLength(50)]
        public string Ativo { get; set; }
        [StringLength(250)]
        public string Equipamento { get; set; }
        [StringLength(1000)]
        public string Contrato { get; set; }
        [StringLength(500)]
        public string LoteImportacao { get; set; }
        [StringLength(500)]
        public string CronogramaInstalacao { get; set; }
        [Column("OS")]
        [StringLength(50)]
        public string Os { get; set; }
        [Column("DtHrOS")]
        [StringLength(50)]
        public string DtHrOs { get; set; }
        [Column("StatusOS")]
        [StringLength(50)]
        public string StatusOs { get; set; }
        [StringLength(50)]
        public string TipoPendencia { get; set; }
        [StringLength(5000)]
        public string MotivoCancelamento { get; set; }
        [Column("NumRAT")]
        [StringLength(50)]
        public string NumRat { get; set; }
        [StringLength(500)]
        public string Tecnico { get; set; }
        [StringLength(5000)]
        public string RelatoSolucao { get; set; }
        [StringLength(5000)]
        public string DefeitoRelatado { get; set; }
        [Column("CNPJ")]
        [StringLength(50)]
        public string Cnpj { get; set; }
        [Column("CEP")]
        [StringLength(50)]
        public string Cep { get; set; }
        [StringLength(50)]
        public string NfPeca { get; set; }
        [StringLength(50)]
        public string NfPecaDataEmissao { get; set; }
        [StringLength(50)]
        public string NfVenda { get; set; }
        [StringLength(50)]
        public string NfVendaDataEmissao { get; set; }
        [StringLength(50)]
        public string Faturamento { get; set; }
        [StringLength(50)]
        public string Termino { get; set; }
    }
}
