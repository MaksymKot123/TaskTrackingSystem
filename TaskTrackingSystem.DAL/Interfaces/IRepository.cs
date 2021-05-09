using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskTrackingSystem.DAL.Models;

namespace TaskTrackingSystem.DAL.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        T Get(int? id);
        IEnumerable<T> GetAll();
        void Create(T item);
        void Edit(T item);
        void Delete(T item);
    }
}