using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TempPecasLogix")]
    public partial class TempPecasLogix
    {
        [Column("cod_filial")]
        public int? CodFilial { get; set; }
        [Column("cod_tecnico")]
        public int CodTecnico { get; set; }
        [Column("cod_fornecedor")]
        [StringLength(50)]
        public string CodFornecedor { get; set; }
        [Column("cpf_tecnico")]
        [StringLength(20)]
        public string CpfTecnico { get; set; }
        [Column("cod_peca")]
        public int CodPeca { get; set; }
        [Column("cod_item")]
        [StringLength(50)]
        public string CodItem { get; set; }
        [Column("den_item")]
        [StringLength(100)]
        public string DenItem { get; set; }
        [Column("nom_tecnico")]
        [StringLength(50)]
        public string NomTecnico { get; set; }
        [Column("dat_emis_nf", TypeName = "datetime")]
        public DateTime? DatEmisNf { get; set; }
        [Column("qtd_solicitada", TypeName = "decimal(38, 2)")]
        public decimal? QtdSolicitada { get; set; }
        [Column("qtd_devolvida", TypeName = "decimal(38, 2)")]
        public decimal? QtdDevolvida { get; set; }
        [Column("qtd_com_tecnico", TypeName = "decimal(38, 2)")]
        public decimal? QtdComTecnico { get; set; }
    }
}
