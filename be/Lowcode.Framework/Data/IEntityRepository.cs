using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lowcode.Framework.Data
{
    public interface IEntityRepository<T, TC>
        where T : class, new()
        where TC : DbContext
    {
        TC GetDbContext();

        Task<T> GetByIdAsync(object id);

        T GetById(object id);

        Task<T> SingleAsync(Expression<Func<T, bool>> query);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> query);

        T Single(Expression<Func<T, bool>> query);

        T FirstOrDefault(Expression<Func<T, bool>> query);

        Task<bool> InsertAsync(T entity);

        bool Insert(T entity);

        Task<bool> DeleteAsync(T entity);

        bool Delete(T entity);

        Task<int> DeleteManyAsync(Expression<Func<T, bool>> expression);

        int DeleteMany(Expression<Func<T, bool>> expression);

        Task<bool> InsertManyAsync(IEnumerable<T> listItems);

        bool InsertMany(IEnumerable<T> listItems);

        Task<bool> UpdateAsync(T entity);

        bool Update(T entity);

        Task<bool> UpdateManyAsync(IEnumerable<T> items);

        bool UpdateMany(IEnumerable<T> items);

        Task<T> AddAsync(T item);

        T Add(T item);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        bool Any(Expression<Func<T, bool>> expression);

        Task<bool> AllAsync(Expression<Func<T, bool>> expression);

        bool All(Expression<Func<T, bool>> expression);

        Task<int> CountAsync();

        int Count();

        Task<int> CountAsync(Expression<Func<T, bool>> query);

        int Count(Expression<Func<T, bool>> query);

        Task<List<T>> FetchAsync();

        List<T> Fetch();

        Task<List<T>> FetchAsync(Expression<Func<T, bool>> query);

        List<T> Fetch(Expression<Func<T, bool>> query);

        Task<List<T>> FetchAsync<TKey>(Expression<Func<T, bool>> query, Expression<Func<T, TKey>> orderBy, PagingInfo paging);

        List<T> Fetch<TKey>(Expression<Func<T, bool>> query, Expression<Func<T, TKey>> orderBy, PagingInfo paging);

        Task<List<T>> FetchAsync<TKey>(Expression<Func<T, TKey>> orderBy, PagingInfo paging);

        List<T> Fetch<TKey>(Expression<Func<T, TKey>> orderBy, PagingInfo paging);

        Task<List<T>> FetchAsync<TKey>(Expression<Func<T, TKey>> orderBy, PagingInfo paging, SortDirection sortDirection);

        List<T> Fetch<TKey>(Expression<Func<T, TKey>> orderBy, PagingInfo paging, SortDirection sortDirection);

        Task<List<T>> FetchAsync<TKey>(Expression<Func<T, bool>> query, Expression<Func<T, TKey>> orderBy, PagingInfo paging, SortDirection sortDirection);

        List<T> Fetch<TKey>(Expression<Func<T, bool>> query, Expression<Func<T, TKey>> orderBy, PagingInfo paging, SortDirection sortDirection);

        Task<List<T>> FetchAsync<TKey>(Expression<Func<T, bool>> query, Expression<Func<T, TKey>> orderBy);

        List<T> Fetch<TKey>(Expression<Func<T, bool>> query, Expression<Func<T, TKey>> orderBy);
    }
}