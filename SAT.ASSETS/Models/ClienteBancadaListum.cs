using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class ClienteBancadaListum
    {
        [Key]
        public int CodClienteBancada { get; set; }
        [Key]
        public int CodBancadaLista { get; set; }

        [ForeignKey(nameof(CodBancadaLista))]
        [InverseProperty(nameof(BancadaListum.ClienteBancadaLista))]
        public virtual BancadaListum CodBancadaListaNavigation { get; set; }
        [ForeignKey(nameof(CodClienteBancada))]
        [InverseProperty(nameof(ClienteBancadum.ClienteBancadaLista))]
        public virtual ClienteBancadum CodClienteBancadaNavigation { get; set; }
    }
}
