using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwEstruturaPeca
    {
        [Column("cod_item_pai")]
        [StringLength(15)]
        public string CodItemPai { get; set; }
        [Required]
        [Column("cod_item_compon")]
        [StringLength(15)]
        public string CodItemCompon { get; set; }
        [Column("qtd_necessaria")]
        [StringLength(50)]
        public string QtdNecessaria { get; set; }
        [Required]
        [StringLength(80)]
        public string NomePeca { get; set; }
    }
}
