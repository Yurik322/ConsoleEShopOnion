using System.Collections.Generic;

namespace OA_DataAccess.Interfaces
{
    /// <summary>
    /// Interface for current users orders.
    /// </summary>
    /// <typeparam name="T">parameter list of orders.</typeparam>
    public interface IOrderRepository<T> : IRepository<T> where T : class
    {
        List<T> CurrentUsersOrders(int userId);
    }
}
