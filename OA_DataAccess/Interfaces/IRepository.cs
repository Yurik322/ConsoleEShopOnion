using System.Collections.Generic;

namespace OA_DataAccess.Interfaces
{
    /// <summary>
    /// IRepository pattern for encapsulating the logic of working with data sources.
    /// </summary>
    /// <typeparam name="T">parameter of model.</typeparam>
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Create(T item);
        void Update(T item);
    }
}
