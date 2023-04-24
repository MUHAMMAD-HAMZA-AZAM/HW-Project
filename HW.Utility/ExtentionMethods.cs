using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HW.Utility
{
    public static class PagingExtensions
    {
        
        //used by LINQ to SQL
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int pageNumber, int pageSize = 10)
        {
            int pagenumber = (pageNumber - 1) * pageSize;
            return source.Skip(pagenumber).Take(pageSize);
            
        }

        //used by LINQ
        //public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int page, int pageSize)
        //{
        //    return source.Skip((page - 1) * pageSize).Take(pageSize);
        //}

    }
}
