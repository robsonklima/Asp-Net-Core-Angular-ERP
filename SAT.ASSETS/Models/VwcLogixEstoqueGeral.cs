using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcLogixEstoqueGeral
    {
        [Required]
        [Column("cod_item_compon")]
        [StringLength(15)]
        public string CodItemCompon { get; set; }
        [Column("qtd_necessaria")]
        public double? QtdNecessaria { get; set; }
    }
}
