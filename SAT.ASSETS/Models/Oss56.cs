using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("OSs56")]
    public partial class Oss56
    {
        [Column("CodOS")]
        public double? CodOs { get; set; }
        public double? CodEquip { get; set; }
        [Column("CNPJEstabelecimentoCliente")]
        [StringLength(255)]
        public string CnpjestabelecimentoCliente { get; set; }
        [StringLength(255)]
        public string NomeArquivoIntegracaoBanrisul { get; set; }
    }
}
