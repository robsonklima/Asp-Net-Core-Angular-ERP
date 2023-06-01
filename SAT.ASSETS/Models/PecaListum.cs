using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class PecaListum
    {
        public PecaListum()
        {
            Clientes = new HashSet<Cliente>();
            PecaListaPecas = new HashSet<PecaListaPeca>();
        }

        [Key]
        public int CodPecaLista { get; set; }
        [Required]
        [StringLength(50)]
        public string NomePecaLista { get; set; }
        [StringLength(300)]
        public string DescPecaLista { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [InverseProperty(nameof(Cliente.CodPecaListaNavigation))]
        public virtual ICollection<Cliente> Clientes { get; set; }
        [InverseProperty(nameof(PecaListaPeca.CodPecaListaNavigation))]
        public virtual ICollection<PecaListaPeca> PecaListaPecas { get; set; }
    }
}
