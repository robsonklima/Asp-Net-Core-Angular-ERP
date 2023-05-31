using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcDadosConfiabilidade2Old2
    {
        [Column("nomefilial")]
        [StringLength(50)]
        public string Nomefilial { get; set; }
        [Column("codfilial")]
        public int? Codfilial { get; set; }
        [Required]
        [Column("nomefantasia")]
        [StringLength(50)]
        public string Nomefantasia { get; set; }
        [Column("nrocontrato")]
        [StringLength(20)]
        public string Nrocontrato { get; set; }
        [Column("codecausa")]
        [StringLength(5)]
        public string Codecausa { get; set; }
        [Column("codcausa")]
        public int? Codcausa { get; set; }
        [Column("CodECausaAbrev")]
        [StringLength(3)]
        public string CodEcausaAbrev { get; set; }
        [Column("defeito")]
        [StringLength(50)]
        public string Defeito { get; set; }
        [Column("acao")]
        [StringLength(50)]
        public string Acao { get; set; }
        [Column("defeitorelatado")]
        [StringLength(3500)]
        public string Defeitorelatado { get; set; }
        [Column("relatosolucao")]
        [StringLength(1000)]
        public string Relatosolucao { get; set; }
        [Required]
        [Column("nomelocal")]
        [StringLength(50)]
        public string Nomelocal { get; set; }
        [Required]
        [Column("numagencia")]
        [StringLength(5)]
        public string Numagencia { get; set; }
        [Column("dcposto")]
        [StringLength(4)]
        public string Dcposto { get; set; }
        [Column("codequip")]
        public int Codequip { get; set; }
        [Required]
        [Column("nomeequip")]
        [StringLength(50)]
        public string Nomeequip { get; set; }
        [Column("codmagnus")]
        [StringLength(20)]
        public string Codmagnus { get; set; }
        [Column("numserie")]
        [StringLength(20)]
        public string Numserie { get; set; }
        [Required]
        [StringLength(1)]
        public string Ind { get; set; }
        [Column("datahoradefeito", TypeName = "datetime")]
        public DateTime? Datahoradefeito { get; set; }
        [Column("datadefeito")]
        [StringLength(92)]
        public string Datadefeito { get; set; }
        [Column("horadefeito")]
        [StringLength(30)]
        public string Horadefeito { get; set; }
        [Column("qtddia")]
        public int? Qtddia { get; set; }
        [Column("dataativacao", TypeName = "datetime")]
        public DateTime? Dataativacao { get; set; }
        [Column("os")]
        public int? Os { get; set; }
        [Column("datahoraaberturaos", TypeName = "datetime")]
        public DateTime? Datahoraaberturaos { get; set; }
        [Column("datahorasolicatendimento", TypeName = "datetime")]
        public DateTime? Datahorasolicatendimento { get; set; }
        [Column("datahorainicio", TypeName = "datetime")]
        public DateTime? Datahorainicio { get; set; }
        [Column("datahorafim", TypeName = "datetime")]
        public DateTime? Datahorafim { get; set; }
        [Column("tempoconserto")]
        public double? Tempoconserto { get; set; }
        [Column("tempomaquinaindisponivel", TypeName = "decimal(10, 4)")]
        public decimal? Tempomaquinaindisponivel { get; set; }
        [Column("tecnicorat")]
        [StringLength(50)]
        public string Tecnicorat { get; set; }
        [Column("foneperto")]
        [StringLength(16)]
        public string Foneperto { get; set; }
        [Column("celular")]
        [StringLength(40)]
        public string Celular { get; set; }
        [Column("foneparticular")]
        [StringLength(16)]
        public string Foneparticular { get; set; }
        [Column("usuariocadastrorat")]
        [StringLength(50)]
        public string Usuariocadastrorat { get; set; }
        [Column("lote")]
        [StringLength(50)]
        public string Lote { get; set; }
        [Column("servico")]
        [StringLength(50)]
        public string Servico { get; set; }
    }
}
