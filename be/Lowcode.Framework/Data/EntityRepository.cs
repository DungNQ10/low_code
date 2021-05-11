using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.EntityFrameworkCore.Design;

namespace Lowcode.Framework.Data
{
    public class EntityRepository<T, TC> : IEntityRepository<T, TC>
        where T : class, new()
        where TC : DbContext, new()
    {
        public EntityRepository()
        {
        }

        public async Task<T> GetByIdAsync(object id)
        {
            using (var context = new TC())
            {                
                return await context.Set<T>().FindAsync(id); 
            }
        }

        public async Task<bool> InsertAsync(T entity)
        {
            using (var context = new TC())
            {
                context.Set<T>().Add(entity);
                var res = await context.SaveChangesAsync();
                if (res > 0)
                    return true;
                return false; 
            }
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            using (var context = new TC())
            {
                context.Set<T>().Remove(entity);
                var res = await context.SaveChangesAsync();
                if (res > 0)
                    return true;
                return false; 
            }
        }

        public async Task<int> DeleteManyAsync(Expression<Func<T, bool>> expression)
        {
            using (var context = new TC())
            {
                IQueryable<T> result = context.Set<T>().Where(expression);

                foreach (T item in result)
                {
                    context.Set<T>().Remove(item);
                }

                var res = await context.SaveChangesAsync();
                return res; 
            }
        }

        public async Task<bool> InsertManyAsync(IEnumerable<T> insertList)
        {
            using (var context = new TC())
            {
                foreach (T item in insertList)
                {
                    context.Set<T>().Add(item);
                }

                var res = await context.SaveChangesAsync();
                if (res > 0)
                    return true;
                return false; 
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            using (var context = new TC())
            {
                context.Set<T>().Attach(entity);
                context.Entry<T>(entity).State = EntityState.Modified;

                var res = await context.SaveChangesAsync();
                if (res > 0)
                    return true;
                return false; 
            }
        }

        public async Task<bool> UpdateManyAsync(IEnumerable<T> items)
        {
            using (var context = new TC())
            {
                foreach (T item in items)
                {
                    context.Set<T>().Attach(item);
                    context.Entry<T>(item).State = EntityState.Modified;
                }

                var res = await context.SaveChangesAsync();
                if (res > 0)
                    return true;
                return false; 
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            using (var context = new TC())
            {
                context.Set<T>().Add(entity);
                var res = await context.SaveChangesAsync();
                if (res > 0)
                    return entity;
                return null; 
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            using (var context = new TC())
            {
                var res = await context.Set<T>().AnyAsync(expression);
                return res; 
            }
        }

        public async Task<bool> AllAsync(Expression<Func<T, bool>> expression)
        {
            using (var context = new TC())
            {
                var res = await context.Set<T>().AllAsync(expression);
                return res; 
            }
        }

        public async Task<T> SingleAsync(Expression<Func<T, bool>> query)
        {
            using (var context = new TC())
            {
                return await context.Set<T>().SingleOrDefaultAsync(query); 
            }
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> query)
        {
            using (var context = new TC())
            {
                return await context.Set<T>().FirstOrDefaultAsync(query); 
            }
        }

        public async Task<List<T>> FetchAsync()
        {
            using (var context = new TC())
            {
                List<T> result = await context.Set<T>().ToListAsync();
                return result; 
            }
        }

        public async Task<List<T>> FetchAsync<TKey>(Expression<Func<T, bool>> query, Expression<Func<T, TKey>> orderBy, PagingInfo paging)
        {
            if (orderBy == null)
            {
                throw new ArgumentNullException("OrderBy is not NULL");
            }

            if (paging == null)
            {
                throw new ArgumentNullException("PagingInfo is not NULL");
            }

            if (paging.CurrentPage < 1)
            {
                throw new ArgumentNullException("CurrentPage in not less than 1");
            }

            using (var context = new TC())
            {
                List<T> result = new List<T>();

                if (query != null)
                {
                    paging.RowsCount = await context.Set<T>().Where(query).CountAsync();

                    result = await context.Set<T>().Where(query).OrderBy(orderBy).Skip(paging.PageSize * (paging.CurrentPage - 1)).Take(paging.PageSize).ToListAsync();

                }
                else
                {
                    paging.RowsCount = await context.Set<T>().CountAsync();

                    result = await context.Set<T>().OrderBy(orderBy).Skip(paging.PageSize * (paging.CurrentPage - 1)).Take(paging.PageSize).ToListAsync();
                }

                return result;
            }//using
        }

        public async Task<List<T>> FetchAsync<TKey>(Expression<Func<T, TKey>> orderBy, PagingInfo paging)
        {
            return await FetchAsync(null, orderBy, paging);
        }

        public async Task<List<T>> FetchAsync<TKey>(Expression<Func<T, TKey>> orderBy, PagingInfo paging, SortDirection sortDirection)
        {
            return await FetchAsync(null, orderBy, paging, sortDirection);
        }

        public async Task<List<T>> FetchAsync<TKey>(Expression<Func<T, bool>> query, Expression<Func<T, TKey>> orderBy, PagingInfo paging, SortDirection sortDirection)
        {
            if (orderBy == null)
            {
                throw new ArgumentNullException("OrderBy is not NULL");
            }

            if (paging == null)
            {
                throw new ArgumentNullException("PagingInfo is not NULL");
            }

            if (paging.CurrentPage < 1)
            {
                throw new ArgumentNullException("CurrentPage is not less than 1");
            }

            using (var db = new TC())
            {
                List<T> result = new List<T>();

                if (query != null)
                {
                    paging.RowsCount = await db.Set<T>().Where(query).CountAsync();

                    if (sortDirection == SortDirection.Ascending)
                    {
                        result = await db.Set<T>().Where(query).OrderBy(orderBy).Skip(paging.PageSize * (paging.CurrentPage - 1)).Take(paging.PageSize).ToListAsync();
                    }
                    else
                    {
                        var q = db.Set<T>().Where(query);
                        var q2 = q.OrderByDescending(orderBy);
                        var sk = q2.Skip(paging.PageSize * (paging.CurrentPage - 1));
                        var tk = sk.Take(paging.PageSize);
                        result = await tk.ToListAsync();
                    }
                }
                else
                {
                    paging.RowsCount = await db.Set<T>().CountAsync();

                    if (sortDirection == SortDirection.Ascending)
                    {
                        result = await
                            db.Set<T>().OrderBy(orderBy).Skip(paging.PageSize * (paging.CurrentPage - 1)).Take(
                                paging.PageSize).ToListAsync();
                    }
                    else
                    {
                        result = await
                           db.Set<T>().OrderByDescending(orderBy).Skip(paging.PageSize * (paging.CurrentPage - 1)).Take(
                               paging.PageSize).ToListAsync();
                    }
                }

                return result;
            }//using
        }

        public async Task<List<T>> FetchAsync<TKey>(Expression<Func<T, bool>> query, Expression<Func<T, TKey>> orderBy)
        {
            using (var db = new TC())
            {
                List<T> result = new List<T>();

                if (query != null)
                {
                    result = await db.Set<T>().Where(query).OrderBy(orderBy).ToListAsync();
                }
                else
                {
                    result = await db.Set<T>().OrderBy(orderBy).ToListAsync();
                }

                return result;
            }//using
        }

        public TC GetDbContext()
        {
            TC db = new TC();
            return db;
        }

        public async Task<List<T>> FetchAsync(Expression<Func<T, bool>> query)
        {
            using (var db = new TC())
            {
                List<T> result = new List<T>();

                if (query != null)
                {
                    result = await db.Set<T>().Where(query).ToListAsync();
                }
                else
                {
                    result = await db.Set<T>().ToListAsync();
                }

                return result;
            }//using
        }

        public T GetById(object id)
        {
            using (var context = new TC())
            {
                return context.Set<T>().Find(id);
            }
        }

        public T Single(Expression<Func<T, bool>> query)
        {
            using (var db = new TC())
            {
                return db.Set<T>().SingleOrDefault(query);
            }
        }

        public T FirstOrDefault(Expression<Func<T, bool>> query)
        {
            using (var db = new TC())
            {
                return db.Set<T>().FirstOrDefault(query);
            }
        }

        public bool Insert(T entity)
        {
            using (var db = new TC())
            {
                db.Set<T>().Add(entity);
                var res = db.SaveChanges();
                if (res > 0)
                    return true;
                return false;
            }
        }

        public bool Delete(T entity)
        {
            using (var db = new TC())
            {
                try
                {
                    db.Set<T>().Remove(entity);
                    var res = db.SaveChanges();
                    if (res > 0)
                        return true;
                    return false;
                }
                catch (Exception ex)
                {

                    throw;
                }
               
            }
        }

        public int DeleteMany(Expression<Func<T, bool>> expression)
        {
            using (var db = new TC())
            {
                IQueryable<T> result = db.Set<T>().Where(expression);

                foreach (T item in result)
                {
                    db.Set<T>().Remove(item);
                }

                var res = db.SaveChanges();
                return res;
            }//using
        }

        public bool InsertMany(IEnumerable<T> insertList)
        {
            using (var db = new TC())
            {
                foreach (T item in insertList)
                {
                    db.Set<T>().Add(item);
                }

                var res = db.SaveChanges();
                if (res > 0)
                    return true;
                return false;
            }//using
        }

        public bool Update(T entity)
        {
            using (var db = new TC())
            {
                db.Set<T>().Attach(entity);

                db.Entry<T>(entity).State = EntityState.Modified;

                var res = db.SaveChanges();
                if (res > 0)
                    return true;

                return false;
            }//using
        }

        public bool UpdateMany(IEnumerable<T> items)
        {
            using (var db = new TC())
            {
                foreach (T item in items)
                {
                    db.Set<T>().Attach(item);
                    db.Entry<T>(item).State = EntityState.Modified;
                }

                var res = db.SaveChanges();
                if (res > 0)
                    return true;
                return false;
            }//using
        }

        public T Add(T entity)
        {
            using (var db = new TC())
            {
                db.Set<T>().Add(entity);
                var res = db.SaveChanges();
                if (res > 0)
                    return entity;
                return null;
            }
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            using (var db = new TC())
            {
                var res = db.Set<T>().Any(expression);
                return res;
            }
        }

        public bool All(Expression<Func<T, bool>> expression)
        {
            using (var db = new TC())
            {
                var res = db.Set<T>().All(expression);
                return res;
            }
        }

        public List<T> Fetch()
        {
            using (var db = new TC())
            {
                List<T> result = db.Set<T>().ToList();

                return result;
            }//using
        }

        public List<T> Fetch(Expression<Func<T, bool>> query)
        {
            using (var db = new TC())
            {
                List<T> result = new List<T>();

                if (query != null)
                {
                    result = db.Set<T>().Where(query).ToList();
                }
                else
                {
                    result = db.Set<T>().ToList();
                }

                return result;
            }//using
        }

        public List<T> Fetch<TKey>(Expression<Func<T, bool>> query, Expression<Func<T, TKey>> orderBy, PagingInfo paging)
        {
            if (orderBy == null)
            {
                throw new ArgumentNullException("OrderBy is not NULL");
            }

            if (paging == null)
            {
                throw new ArgumentNullException("PagingInfo is not NULL");
            }

            if (paging.CurrentPage < 1)
            {
                throw new ArgumentNullException("CurrentPage in not less than 1");
            }

            using (var db = new TC())
            {
                List<T> result = new List<T>();

                if (query != null)
                {
                    paging.RowsCount = db.Set<T>().Where(query).Count();

                    result = db.Set<T>().Where(query).OrderBy(orderBy).Skip(paging.PageSize * (paging.CurrentPage - 1)).Take(paging.PageSize).ToList();


                }
                else
                {
                    paging.RowsCount = db.Set<T>().Count();

                    result = db.Set<T>().OrderBy(orderBy).Skip(paging.PageSize * (paging.CurrentPage - 1)).Take(paging.PageSize).ToList();
                }

                return result;
            }//using
        }

        public List<T> Fetch<TKey>(Expression<Func<T, TKey>> orderBy, PagingInfo paging)
        {
            return Fetch(null, orderBy, paging);
        }

        public List<T> Fetch<TKey>(Expression<Func<T, TKey>> orderBy, PagingInfo paging, SortDirection sortDirection)
        {
            return Fetch(null, orderBy, paging, sortDirection);
        }

        public List<T> Fetch<TKey>(Expression<Func<T, bool>> query, Expression<Func<T, TKey>> orderBy, PagingInfo paging, SortDirection sortDirection)
        {
            if (orderBy == null)
            {
                throw new ArgumentNullException("OrderBy is not NULL");
            }

            if (paging == null)
            {
                throw new ArgumentNullException("PagingInfo is not NULL");
            }

            if (paging.CurrentPage < 1)
            {
                throw new ArgumentNullException("CurrentPage is not less than 1");
            }

            using (var db = new TC())
            {
                List<T> result = new List<T>();

                if (query != null)
                {
                    paging.RowsCount = db.Set<T>().Where(query).Count();

                    if (sortDirection == SortDirection.Ascending)
                    {
                        result = db.Set<T>().Where(query).OrderBy(orderBy).Skip(paging.PageSize * (paging.CurrentPage - 1)).Take(paging.PageSize).ToList();
                    }
                    else
                    {
                        var q = db.Set<T>().Where(query);
                        var q2 = q.OrderByDescending(orderBy);
                        var sk = q2.Skip(paging.PageSize * (paging.CurrentPage - 1));
                        var tk = sk.Take(paging.PageSize);
                        result = tk.ToList();
                    }
                }
                else
                {
                    paging.RowsCount = db.Set<T>().Count();

                    if (sortDirection == SortDirection.Ascending)
                    {
                        result =
                            db.Set<T>().OrderBy(orderBy).Skip(paging.PageSize * (paging.CurrentPage - 1)).Take(
                                paging.PageSize).ToList();
                    }
                    else
                    {
                        result =
                           db.Set<T>().OrderByDescending(orderBy).Skip(paging.PageSize * (paging.CurrentPage - 1)).Take(
                               paging.PageSize).ToList();
                    }
                }

                return result;
            }//using
        }

        public List<T> Fetch<TKey>(Expression<Func<T, bool>> query, Expression<Func<T, TKey>> orderBy)
        {
            using (var db = new TC())
            {
                List<T> result = new List<T>();

                if (query != null)
                {
                    result = db.Set<T>().Where(query).OrderBy(orderBy).ToList();
                }
                else
                {
                    result = db.Set<T>().OrderBy(orderBy).ToList();
                }

                return result;
            }//using
        }

        public async Task<int> CountAsync()
        {
            using (var db = new TC())
            {
                return await db.Set<T>().CountAsync();
            }//using
        }

        public int Count()
        {
            using (var db = new TC())
            {
                return db.Set<T>().Count();
            }//using
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> query)
        {
            using (var db = new TC())
            {
                return await db.Set<T>().Where(query).CountAsync();
            }//using
        }

        public int Count(Expression<Func<T, bool>> query)
        {
            using (var db = new TC())
            {
                return db.Set<T>().Where(query).Count();
            }//using
        }
    }
}