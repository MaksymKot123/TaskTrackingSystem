using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskTrackingSystem.DAL.Models;

namespace TaskTrackingSystem.DAL.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// Get T entity
        /// </summary>
        /// <param name="name"></param>
        /// <returns>T entity</returns>
        T Get(string name);

        /// <summary>
        /// Get all T entities
        /// </summary>
        /// <returns>a list of T</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Add to database new T entity
        /// </summary>
        /// <param name="item"></param>
        void Create(T item);

        /// <summary>
        /// Edit T entity
        /// </summary>
        /// <param name="item"></param>
        void Edit(T item);

        /// <summary>
        /// Delete T entity from database
        /// </summary>
        /// <param name="item"></param>
        void Delete(T item);
    }
}