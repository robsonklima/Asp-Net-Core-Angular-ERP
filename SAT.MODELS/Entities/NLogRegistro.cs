using System;

namespace SAT.MODELS.Entities
{
    public class NLogRegistro
    {
        public DateTime Time { get; set; }
        public string Level { get; set; }
        public NLogNested Nested { get; set; }
    }

    public class NLogNested
    {
        public string Application { get; set; }
        public string Message { get; set; }
    }
}
