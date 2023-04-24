using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class LogFrontEnd
    {
        public int Level { get; set; }
        public List<object> Additional { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string FileName { get; set; }
        public int LineNumber { get; set; }
        public int ColumnNumber { get; set; }
    }

}