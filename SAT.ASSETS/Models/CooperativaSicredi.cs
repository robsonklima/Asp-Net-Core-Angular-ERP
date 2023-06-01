using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("CooperativaSicredi")]
    public partial class CooperativaSicredi
    {
        public CooperativaSicredi()
        {
            FaturamentoSicrediMultaSaldos = new HashSet<FaturamentoSicrediMultaSaldo>();
            NotaFaturamentoSicredis = new HashSet<NotaFaturamentoSicredi>();
            PatrimonioPos = new HashSet<PatrimonioPo>();
        }

        [Key]
        public int CodCooperativaSicredi { get; set; }
        [Required]
        [StringLength(50)]
        public string CodCooperativa { get; set; }
        [Required]
        [StringLength(250)]
        public string NomeCooperativa { get; set; }
        [StringLength(50)]
        public string Cnpj { get; set; }
        [StringLength(250)]
        public string NomeFantasia { get; set; }
        [StringLength(250)]
        public string NomeReduzido { get; set; }
        [StringLength(300)]
        public string Endereco { get; set; }
        [StringLength(150)]
        public string Bairro { get; set; }
        [StringLength(250)]
        public string Cidade { get; set; }
        [Column("UF")]
        [StringLength(50)]
        public string Uf { get; set; }
        [StringLength(50)]
        public string Cep { get; set; }
        [StringLength(50)]
        public string Telefone { get; set; }
        [StringLength(250)]
        public string Central { get; set; }
        [StringLength(50)]
        public string Credis { get; set; }

        [InverseProperty(nameof(FaturamentoSicrediMultaSaldo.CodCooperativaSicrediNavigation))]
        public virtual ICollection<FaturamentoSicrediMultaSaldo> FaturamentoSicrediMultaSaldos { get; set; }
        [InverseProperty(nameof(NotaFaturamentoSicredi.CodCooperativaSicrediNavigation))]
        public virtual ICollection<NotaFaturamentoSicredi> NotaFaturamentoSicredis { get; set; }
        [InverseProperty(nameof(PatrimonioPo.CodCooperativaSicrediNavigation))]
        public virtual ICollection<PatrimonioPo> PatrimonioPos { get; set; }
    }
}
