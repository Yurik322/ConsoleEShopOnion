using OA_DataAccess.EF;
using OA_DataAccess.Entities;
using OA_DataAccess.Interfaces;

namespace OA_DataAccess.Repositories
{
    /// <summary>
    /// Unit of Work pattern simplifies working with different repositories for getting data from repository.
    /// It Helps work with data context.
    /// </summary>
    public class EfUnitOfWork : IUnitOfWorks
    {
        private readonly EShopContext _db;
        private ProductRepository _productRepository;
        private UserRepository _userRepository;
        private OrderRepository _orderRepository;

        public EfUnitOfWork()
        {
            _db = new EShopContext();
        }

        public IRepository<Product> Products
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_db);
                }
                return _productRepository;
            }
        }

        public IOrderRepository<Order> Orders
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new OrderRepository(_db);
                }
                return _orderRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_db);
                }
                return _userRepository;
            }
        }
    }
}
