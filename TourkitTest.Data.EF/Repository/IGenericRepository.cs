// "/*
// * -----------------------------------------------------------------------------
// * File name: IGenericRepository.cs
// * Create date: 
// * Update date: 
// * Author: 
// *-----------------------------------------------------------------------------
// * Copyright (c) TruongNX-Tourkit. All rights reserved.
// * -----------------------------------------------------------------------------
// */"

using System.Linq.Expressions;

namespace TourkitTest.Data.EF.Repository
{
    public interface IGenericRepository<T> : IDisposable where T : class 
    {
        T Add(T t);
        Task<T> AddAsyn(T t);
        bool Any();
        Task<bool> AnyAsync();
        int Count();
        Task<int> CountAsync(Expression<Func<T, bool>> filter = null);
        void Delete(T entity);
        Task<int> DeleteAsyn(T entity);
#pragma warning disable S2953 // Methods named "Dispose" should implement "IDisposable.Dispose"
        void Dispose();
#pragma warning restore S2953 // Methods named "Dispose" should implement "IDisposable.Dispose"
        T Find(Expression<Func<T, bool>> match);
        ICollection<T> FindAll(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate);
        T Get(object id);
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsyn();
        Task<IEnumerable<T>> GetAllAsNoTrackingAsyn();
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(params object[] id);
        void Save();
        Task<int> SaveAsync();
        T Update(T t, object key);
        Task<T> UpdateAsyn(T t, object key);
    }
}
