using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcCesarBessaExpEquipamento
    {
        [Column("ANOMES")]
        public int? Anomes { get; set; }
        public int? CodEquipContrato { get; set; }
        [Column("GRUPO")]
        [StringLength(50)]
        public string Grupo { get; set; }
        [Column("TIPO")]
        [StringLength(50)]
        public string Tipo { get; set; }
        [Column("MODELO")]
        [StringLength(50)]
        public string Modelo { get; set; }
        [Column("CODIGOPAI")]
        [StringLength(20)]
        public string Codigopai { get; set; }
        [Column("NUMAGENCIA")]
        [StringLength(6)]
        public string Numagencia { get; set; }
        [Column("DCPOSTO")]
        [StringLength(4)]
        public string Dcposto { get; set; }
        [Column("NOMELOCAL")]
        [StringLength(50)]
        public string Nomelocal { get; set; }
        [Column("ENDERECO")]
        [StringLength(150)]
        public string Endereco { get; set; }
        [Column("CIDADE")]
        [StringLength(50)]
        public string Cidade { get; set; }
        [Column("UF")]
        [StringLength(50)]
        public string Uf { get; set; }
        [Column("NOMEFILIAL")]
        [StringLength(50)]
        public string Nomefilial { get; set; }
        [Column("CLIENTE")]
        [StringLength(50)]
        public string Cliente { get; set; }
        [Column("PAT")]
        [StringLength(50)]
        public string Pat { get; set; }
        [Column("REGIAO")]
        [StringLength(50)]
        public string Regiao { get; set; }
        [Column("CONTRATO")]
        [StringLength(100)]
        public string Contrato { get; set; }
        [Column("SLA")]
        [StringLength(50)]
        public string Sla { get; set; }
        [Column("DistanciaKmPAT_Res", TypeName = "decimal(10, 2)")]
        public decimal? DistanciaKmPatRes { get; set; }
        [Column("PA")]
        public int? Pa { get; set; }
        [Column("TIPOCONTRATO")]
        [StringLength(50)]
        public string Tipocontrato { get; set; }
        [Required]
        [Column("INDATIVO")]
        [StringLength(3)]
        public string Indativo { get; set; }
        [Required]
        [Column("INDRECEITA")]
        [StringLength(3)]
        public string Indreceita { get; set; }
        [Required]
        [Column("INDREPASSE")]
        [StringLength(3)]
        public string Indrepasse { get; set; }
        [Required]
        [Column("INDGARANTIA")]
        [StringLength(3)]
        public string Indgarantia { get; set; }
        [Required]
        [Column("INDINSTALACAO")]
        [StringLength(3)]
        public string Indinstalacao { get; set; }
        [Column("VALORRECEITA", TypeName = "money")]
        public decimal? Valorreceita { get; set; }
        [Column("VALORDESPESA", TypeName = "money")]
        public decimal? Valordespesa { get; set; }
    }
}
