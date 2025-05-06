using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Entities.Base;
using MediatR;

namespace AlfaLogger.Repository.Extensions
{


    internal static class QueryableExtensions
    {
    
       internal static IQueryable<T> ApplyFilters<T>(
            this IQueryable<T> query,
            IEnumerable<Expression<Func<T, bool>>>? filters) where T : Entity, new()
        {
            if (filters is not null)
                foreach (var filter in filters)
                    query = query.Where(filter);
            return query;
        }

        internal static IQueryable<T> ApplyInclude<T>(
            this IQueryable<T> query,
            IEnumerable<Expression<Func<T, object>>>? includes) where T : Entity, new()
        {
            if (includes is not null)
                foreach (var include in includes)
                    query = query.Include(include);
            return query;
        }

        internal static IQueryable<T> OrderByDesc<T>(
            this IQueryable<T> query) where T : Entity, new()
        {
            return query.OrderByDescending(x => x.Id);
        }

        internal static IQueryable<T> Page<T>(
            this IQueryable<T> query,
            int pageNumZeroStart,
            int pageSize) where T : Entity, new()
        {
            if (pageSize == 0)
                return query;

            if(pageNumZeroStart != 0)
                query = query.Skip(pageNumZeroStart * pageSize);

            return query.Take(pageSize);
        }


    }
}
