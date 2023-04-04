using HS4_BlogProject.Domain.Entities;
using HS4_BlogProject.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        /* CRUD işlemlerini yapacağımız metotları barındırdığımız class'ımız.
         * Bu metotlar muhakkak DBSet yani veritabanındaki varlıkları, uygulama tarafındaki yani application tarafındaki karşılıklarına ve ORM gereği veritabanın uygulama tarafındaki karşılığı olan Context nesnemize uğramak zorunda.
         * 
         * Bağlımlılığa neden olan AppDbContext nesnemi Inject ediyorum         
         */

        private readonly AppDbContext _appDbContext;
        protected DbSet<T> table;

        public BaseRepository(AppDbContext appDbContext) // (PostgreAppDbContext:AppDbContext) 
        {
            _appDbContext = appDbContext;
            table = _appDbContext.Set<T>(); // database hangi tipe bürünmüşse T olarak buraya atıyoruz. Table'ın DbSet'ine atıyoruz. Buradaki metotlar tüm entityler için ortak bir şekilde çalışacağı için bunları yapıyoruz. Dependency Injection yaptık.
        }

        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            return await table.AnyAsync(expression);
        }

        public async Task Create(T entity)
        {
            table.Add(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            await _appDbContext.SaveChangesAsync(); //servis katmanında entity 'sine göre pasif hale getiricez.
        }

        public async Task<T> GetDefault(Expression<Func<T, bool>> expression)
        {
            return await table.FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression)
        {
            return await table.Where(expression).ToListAsync();
        }

        public async Task<TResult> GetFilteredFirstOrDefault<TResult>(
                    Expression<Func<T, TResult>> select, 
                    Expression<Func<T, bool>> where, 
                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
                    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = table;  // Select * from  Post

            if (where != null)
                query = query.Where(where); // Select * from Post where Status=1

            if (include != null)
                query = include(query);

            if (orderBy != null)
                return await orderBy(query).Select(select).FirstOrDefaultAsync();
            else
                return await query.Select(select).FirstOrDefaultAsync();
        }

        public async Task<List<TResult>> GetFilteredList<TResult>(
                    Expression<Func<T, TResult>> select, 
                    Expression<Func<T, bool>> where, 
                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
                    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = table;  // Select * from  Post

            if (where != null)
                query = query.Where(where); // Select * from Post where Status=1

            if (include != null)
                query = include(query);

            if (orderBy != null)
                return await orderBy(query).Select(select).ToListAsync();
            else
                return await query.Select(select).ToListAsync();
        }

        public async Task Update(T entity)
        {
            _appDbContext.Entry<T>(entity).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
