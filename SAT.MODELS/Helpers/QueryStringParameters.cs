namespace SAT.MODELS.Entities.Helpers
{
    public abstract class QueryStringParameters
    {
        const int maxPageSize = 100;
        public int PageNumber { get; set; } = 1;
        public string Filter { get; set; }
        public string SortActive { get; set; }
        public string SortDirection { get; set; }
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
               _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
