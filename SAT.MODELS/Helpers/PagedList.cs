using System;
using System.Collections.Generic;
using System.Linq;

namespace SAT.MODELS.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            List<T> L = new List<T>(TotalCount);
            foreach (var item in items)
            {
                L.Add(item);
            }

            var o = items.ToList();
            //    L.AddRange(items);

            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            try
            {
                var count = source.Count();
                var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                return new PagedList<T>(items, count, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static PagedList<T> ToPagedList(List<T> source, int pageNumber, int pageSize)
        {
            try
            {
                var count = source.Count();
                var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                return new PagedList<T>(items, count, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
