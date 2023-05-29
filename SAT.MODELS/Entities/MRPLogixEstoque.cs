namespace SAT.MODELS.Entities
{
    public class MRPLogixEstoque
    {
        public int CodMRPLogixEstoque { get; set; }
        public string CodEmpresa { get; set; }
        public string CodItem { get; set; }
        public string LocalEstoque { get; set; }
        public string NumLote { get; set; }
        public string Situacao { get; set; }   
        public double? QtdSaldo { get; set; }     
        public int? NumTransacao { get; set; }                
    }
}