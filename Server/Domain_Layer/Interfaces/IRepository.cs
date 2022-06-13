using Domain_Layer.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain_Layer.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        void CreateAsync(T entity);
        void DeleteAsync(Guid Id);
        IReadOnlyCollection<T> GetAllAsync();
        IReadOnlyCollection<T> GetAllAsync(Expression<Func<T, bool>> filter);
        T GetAsync(Guid Id);
        T GetAsync(Expression<Func<T,bool>> filter);
        void UpdateAsync(T entity);
    }
}