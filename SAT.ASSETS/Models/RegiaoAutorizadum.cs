using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class RegiaoAutorizadum
    {
        public RegiaoAutorizadum()
        {
            Osatendida = new HashSet<Osatendida>();
        }

        [Key]
        public int CodRegiao { get; set; }
        [Key]
        public int CodAutorizada { get; set; }
        [Key]
        public int CodFilial { get; set; }
        public int? CodCidade { get; set; }
        [Column("PA")]
        public int? Pa { get; set; }
        public byte IndAtivo { get; set; }
        [StringLength(100)]
        public string Endereco { get; set; }
        [StringLength(20)]
        public string Bairro { get; set; }
        [StringLength(20)]
        public string NumeroEnd { get; set; }
        [StringLength(20)]
        public string ComplemEnd { get; set; }
        [Column("CNPJ")]
        [StringLength(20)]
        public string Cnpj { get; set; }
        [Column("CEP")]
        [StringLength(20)]
        public string Cep { get; set; }

        [InverseProperty("Cod")]
        public virtual ICollection<Osatendida> Osatendida { get; set; }
    }
}
