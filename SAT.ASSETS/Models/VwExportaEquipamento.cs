using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwExportaEquipamento
    {
        [Column("codequipcontrato")]
        public int Codequipcontrato { get; set; }
        [Column("nomegrupoequip")]
        [StringLength(50)]
        public string Nomegrupoequip { get; set; }
        [Column("nometipoequip")]
        [StringLength(50)]
        public string Nometipoequip { get; set; }
        [Column("nomeequip")]
        [StringLength(50)]
        public string Nomeequip { get; set; }
        [Column("numserie")]
        [StringLength(20)]
        public string Numserie { get; set; }
        [Column("numagencia")]
        [StringLength(5)]
        public string Numagencia { get; set; }
        [Column("dcposto")]
        [StringLength(4)]
        public string Dcposto { get; set; }
        [Column("nomelocal")]
        [StringLength(50)]
        public string Nomelocal { get; set; }
        [Column("endereco")]
        [StringLength(150)]
        public string Endereco { get; set; }
        [Column("nomecidade")]
        [StringLength(50)]
        public string Nomecidade { get; set; }
        [Column("siglauf")]
        [StringLength(50)]
        public string Siglauf { get; set; }
        [Column("nomefilial")]
        [StringLength(50)]
        public string Nomefilial { get; set; }
        [Column("nomecliente")]
        [StringLength(50)]
        public string Nomecliente { get; set; }
        [Column("nomepat")]
        [StringLength(50)]
        public string Nomepat { get; set; }
        [Column("nomeregiao")]
        [StringLength(50)]
        public string Nomeregiao { get; set; }
        [Column("nomecontrato")]
        [StringLength(30)]
        public string Nomecontrato { get; set; }
        [Column("nomesla")]
        [StringLength(50)]
        public string Nomesla { get; set; }
        [Column("distanciapat_res", TypeName = "decimal(10, 2)")]
        public decimal? DistanciapatRes { get; set; }
        [Column("pa")]
        public int? Pa { get; set; }
        [Column("nometipocontrato")]
        [StringLength(50)]
        public string Nometipocontrato { get; set; }
        [Required]
        [Column("indativo")]
        [StringLength(3)]
        public string Indativo { get; set; }
        [Required]
        [Column("indreceita")]
        [StringLength(3)]
        public string Indreceita { get; set; }
        [Required]
        [Column("indrepasse")]
        [StringLength(3)]
        public string Indrepasse { get; set; }
        [Required]
        [Column("indgarantia")]
        [StringLength(3)]
        public string Indgarantia { get; set; }
        [Required]
        [Column("indinstalacao")]
        [StringLength(3)]
        public string Indinstalacao { get; set; }
        [Column("dataativacao", TypeName = "datetime")]
        public DateTime? Dataativacao { get; set; }
        [Column("valorreceita", TypeName = "money")]
        public decimal Valorreceita { get; set; }
        [Column("valordespesa", TypeName = "money")]
        public decimal Valordespesa { get; set; }
        [Column("datadesativacao", TypeName = "datetime")]
        public DateTime? Datadesativacao { get; set; }
        [Column("datainicgarantia", TypeName = "datetime")]
        public DateTime? Datainicgarantia { get; set; }
        [Column("datafimgarantia", TypeName = "datetime")]
        public DateTime? Datafimgarantia { get; set; }
        [Column("cnpj")]
        [StringLength(20)]
        public string Cnpj { get; set; }
        [Column("cnpjcontrato")]
        [StringLength(20)]
        public string Cnpjcontrato { get; set; }
        [Column("cnpjfaturamento")]
        [StringLength(20)]
        public string Cnpjfaturamento { get; set; }
    }
}
