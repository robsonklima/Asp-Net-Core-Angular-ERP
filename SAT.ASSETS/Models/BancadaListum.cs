using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class BancadaListum
    {
        public BancadaListum()
        {
            BancadaListaPecas = new HashSet<BancadaListaPeca>();
            ClienteBancadaLista = new HashSet<ClienteBancadaListum>();
        }

        [Key]
        public int CodBancadaLista { get; set; }
        [StringLength(30)]
        public string NomeBancadaLista { get; set; }
        public byte? IndAtivo { get; set; }

        [InverseProperty(nameof(BancadaListaPeca.CodBancadaListaNavigation))]
        public virtual ICollection<BancadaListaPeca> BancadaListaPecas { get; set; }
        [InverseProperty(nameof(ClienteBancadaListum.CodBancadaListaNavigation))]
        public virtual ICollection<ClienteBancadaListum> ClienteBancadaLista { get; set; }
    }
}
