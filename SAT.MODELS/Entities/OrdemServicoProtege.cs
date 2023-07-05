using System.Collections.Generic;

namespace SAT.MODELS.Entities {
    public class OrdemServicoProtege
    {
        public string BusObId { get; set; }
        public List<OrdemServicoProtegeField> Fields { get; set; }
        public bool Persist { get; set; }
    }

    public class BusinessObject
    {
        public string busObId { get; set; }
        public string busObPublicId { get; set; }
        public string busObRecId { get; set; }
        public List<OrdemServicoProtegeField> fields { get; set; }
        public List<OrdemServicoProtegeLink> links { get; set; }
        public object errorCode { get; set; }
        public object errorMessage { get; set; }
        public bool hasError { get; set; }
    }

    public class OrdemServicoProtegeField
    {
        public bool dirty { get; set; }
        public string displayName { get; set; }
        public string fieldId { get; set; }
        public string fullFieldId { get; set; }
        public object html { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }

    public class OrdemServicoProtegeLink
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class OrdemServicoProtegeArmazenados
    {
        public List<BusinessObject> businessObjects { get; set; }
        public bool hasPrompts { get; set; }
        public List<OrdemServicoProtegeLink> links { get; set; }
        public List<object> prompts { get; set; }
        public List<object> searchResultsFields { get; set; }
        public object simpleResults { get; set; }
        public int totalRows { get; set; }
        public bool hasMoreRecords { get; set; }
        public object errorCode { get; set; }
        public object errorMessage { get; set; }
        public bool hasError { get; set; }
    }
}