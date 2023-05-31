using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Produto")]
    public partial class Produto
    {
        public Produto()
        {
            LaboratorioPositems = new HashSet<LaboratorioPositem>();
            ProducaoPositems = new HashSet<ProducaoPositem>();
            ProdutoClientes = new HashSet<ProdutoCliente>();
            ProdutoRedes = new HashSet<ProdutoRede>();
        }

        [Key]
        public int CodProduto { get; set; }
        [Required]
        [StringLength(50)]
        public string CodProdutoLogix { get; set; }
        public int CodEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodTipoEquip { get; set; }
        public bool Producao { get; set; }

        [ForeignKey("CodEquip,CodGrupoEquip,CodTipoEquip")]
        [InverseProperty(nameof(Equipamento.Produtos))]
        public virtual Equipamento Cod { get; set; }
        [InverseProperty(nameof(LaboratorioPositem.CodProdutoNavigation))]
        public virtual ICollection<LaboratorioPositem> LaboratorioPositems { get; set; }
        [InverseProperty(nameof(ProducaoPositem.CodProdutoNavigation))]
        public virtual ICollection<ProducaoPositem> ProducaoPositems { get; set; }
        [InverseProperty(nameof(ProdutoCliente.CodProdutoNavigation))]
        public virtual ICollection<ProdutoCliente> ProdutoClientes { get; set; }
        [InverseProperty(nameof(ProdutoRede.CodProdutoNavigation))]
        public virtual ICollection<ProdutoRede> ProdutoRedes { get; set; }
    }
}
