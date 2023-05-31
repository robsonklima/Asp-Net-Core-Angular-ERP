using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class ClienteSemListum
    {
        [Key]
        public int CodClienteSemLista { get; set; }
        public int? CodCliente { get; set; }
        public int? CodContrato { get; set; }

        [ForeignKey(nameof(CodCliente))]
        [InverseProperty(nameof(Cliente.ClienteSemLista))]
        public virtual Cliente CodClienteNavigation { get; set; }
        [ForeignKey(nameof(CodContrato))]
        [InverseProperty(nameof(Contrato.ClienteSemLista))]
        public virtual Contrato CodContratoNavigation { get; set; }
    }
}
