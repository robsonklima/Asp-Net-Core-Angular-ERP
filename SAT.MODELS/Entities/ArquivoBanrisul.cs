using System;

namespace SAT.MODELS.Entities
{
    public class ArquivoBanrisul
    {
        public int CodGerenciaArquivosBanrisul { get; set; } 
        public byte? IndPDFGerado { get; set; }       
        public int CodOS { get; set; }
        public OrdemServico OrdemServico { get; set; }
        public string NumOSCliente { get; set; }
        public string CaminhoPDF { get; set; }
        public string TextoEmail { get; set; }
        public string AssuntoEmail { get; set; }
        public int CodStatusServico { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}
