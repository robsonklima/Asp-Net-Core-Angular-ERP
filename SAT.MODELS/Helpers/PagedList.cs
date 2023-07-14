using System;
using System.Collections.Generic;
using System.Linq;
using SAT.MODELS.Entities.Constants;

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
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize, IEqualityComparer<T> comparer = null)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(comparer != null ? items?.Distinct(comparer) : items);
        }

        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize, IEqualityComparer<T> comparer = null)
        {
            try
            {
                var count = source.Count();
                var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                return new PagedList<T>(items, count, pageNumber, pageSize, comparer);
            }
            catch (InvalidCastException ex)
            {
                throw new Exception(MsgConst.OCORREU_ERRO, ex);
            }
            catch (Exception ex)
            {
                throw new Exception(MsgConst.OCORREU_ERRO, ex);
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
            catch (InvalidCastException ex)
            {
                throw new Exception(MsgConst.OCORREU_ERRO, ex);
            }
            catch (Exception ex)
            {
                throw new Exception(MsgConst.OCORREU_ERRO, ex);
            }
        }
    }
}