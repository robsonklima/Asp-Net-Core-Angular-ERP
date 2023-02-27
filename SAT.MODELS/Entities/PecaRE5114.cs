using System;

namespace SAT.MODELS.Entities
{
    public class PecaRE5114
    {
        public int CodPecaRe5114 { get; set; }
        public string NumRe5114 { get; set; }
        public int CodPeca { get; set; }
        public string NumSerie { get; set; }
        public string NumPecaCliente { get; set; }
        public string CodUsuarioCadastro { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataManut { get; set; }
        public byte? IndSucata { get; set; }
        public byte? IndDevolver { get; set; }
        public byte? IndMarcacaoEspecial { get; set; }
        public string MotivoDevolucao { get; set; }
        public string MotivoSucata { get; set; }
        public int? CodOsbancada { get; set; }
        public Peca Peca { get; set; }
    }
}