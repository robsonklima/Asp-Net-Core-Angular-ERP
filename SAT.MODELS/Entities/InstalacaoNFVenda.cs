using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class InstalacaoNFVenda
    {
        public int CodInstalNFvenda { get; set; }
        public int CodCliente { get; set; }
        public int NumNFVenda { get; set; }
        public DateTime? DataNFVenda { get; set; }
        public string ObsNFVenda { get; set; }
        public DateTime? DataNFVendaEnvioCliente { get; set; }
        public DateTime? DataNFVendaRecebimentoCliente { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}