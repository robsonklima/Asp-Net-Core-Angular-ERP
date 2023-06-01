using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TAMANHO_BASE_HISTORICO_OBJETOS")]
    public partial class TamanhoBaseHistoricoObjeto
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("NOME_TABELA")]
        [StringLength(50)]
        public string NomeTabela { get; set; }
        [Column("NUMERO_LINHAS")]
        public long? NumeroLinhas { get; set; }
        [Column("RESERVED")]
        [StringLength(12)]
        public string Reserved { get; set; }
        [Column("DATA")]
        [StringLength(12)]
        public string Data { get; set; }
        [Column("TAMANHO_INDEX")]
        [StringLength(12)]
        public string TamanhoIndex { get; set; }
        [Column("UNUSED")]
        [StringLength(12)]
        public string Unused { get; set; }
    }
}
