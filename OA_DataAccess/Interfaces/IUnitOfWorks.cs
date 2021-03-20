using OA_DataAccess.Entities;

namespace OA_DataAccess.Interfaces
{
    /// <summary>
    /// Interface for getting lists from data context.
    /// </summary>
    public interface IUnitOfWorks
    {
        IRepository<User> Users { get; }
        IRepository<Product> Products { get; }
        IOrderRepository<Order> Orders { get; }
    }
}
