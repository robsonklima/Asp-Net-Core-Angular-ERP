using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ModalidadeObsFaturamento")]
    public partial class ModalidadeObsFaturamento
    {
        [Key]
        public int CodModalidadeObsFaturamento { get; set; }
        [Required]
        [StringLength(100)]
        public string DescModalidade { get; set; }
        public int IndAtivo { get; set; }
    }
}
