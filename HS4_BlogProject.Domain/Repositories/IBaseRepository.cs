using HS4_BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Domain.Repositories
{
    public interface IBaseRepository<T> where T : IBaseEntity
    {
        // Task: Create metodu Asenkron olarak çalışacak
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity); // veritabanından silme işlemi yapmam, status'ü pasife çekerim.

        Task<bool> Any(Expression<Func<T, bool>> expression); // bir kayıt var varsa true, yoksa false

        Task<T> GetDefault(Expression<Func<T, bool>> expression);// dinamik olarak where işlemi sağlar. Id ye göre getir.

        Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression); // GenreId = 5 olan postların gelmesini istiyoruz mesela.

        // Hem select hem de order by yapabileceğimiz. Post, Yazar, Comment, like 'ları birlikte çekmek için include etmek gerekir. Bir sorguya birden fazla tablo girecek yani eagerloding kullanacağız.

        // Tek dönecek
        Task<TResult> GetFilteredFirstOrDefault<TResult>(
                Expression<Func<T, TResult>> select,  // select
                Expression<Func<T, bool>> where,  // where 
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,  // sıralama
                Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null); // join
                // Backend e SP yazacakmışız gibi bir çalışma yapıyoruz. inclu eager loading ' e göre join , order by, where , select işlemleri yapılır.

        // Çoklu dönecek

        Task<List<TResult>> GetFilteredList<TResult>(
                  Expression<Func<T, TResult>> select,  // select
                  Expression<Func<T, bool>> where,  // where 
                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,  // sıralama
                  Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null); // join

    }
}
