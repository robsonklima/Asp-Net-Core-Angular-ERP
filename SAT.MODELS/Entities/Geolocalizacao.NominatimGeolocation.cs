using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class Address
    {
        public string city { get; set; }
        public string state_district { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
    }

    public class Extratags
    {
        public string capital { get; set; }
        public string website { get; set; }
        public string wikidata { get; set; }
        public string wikipedia { get; set; }
        public string population { get; set; }
    }

    public class NominatimGeolocation
    {
        public string place_id { get; set; }
        public string licence { get; set; }
        public string osm_type { get; set; }
        public string osm_id { get; set; }
        public List<string> boundingbox { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string display_name { get; set; }
        public string @class { get; set; }
        public string type { get; set; }
        public double importance { get; set; }
        public string icon { get; set; }
        public Address address { get; set; }
        public Extratags extratags { get; set; }
    }
}