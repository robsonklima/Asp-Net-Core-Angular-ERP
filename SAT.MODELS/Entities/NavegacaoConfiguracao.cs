﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class NavegacaoConfiguracao
    {
        public int CodNavegacao { get; set; }
        public int CodPerfil { get; set; }
        [ForeignKey("CodNavegacao")]
        public Navegacao Navegacao { get; set; }
        [ForeignKey("CodPerfil")]
        public Perfil Perfil { get; set; }
    }
}
