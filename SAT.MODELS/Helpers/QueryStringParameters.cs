using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities.Helpers
{
    public abstract class QueryStringParameters
    {
        public int PageNumber { get; set; } = 1;
        public string Filter { get; set; }
        public string SortActive { get; set; }
        public string SortDirection { get; set; }
        public bool SortByDesc { get; set; }
        private int? _pageSize;
        public int PageSize
        {
            get
            {
                return _pageSize ?? int.MaxValue;
            }
            set
            {
               _pageSize = value;
            }
        }
    }
}
