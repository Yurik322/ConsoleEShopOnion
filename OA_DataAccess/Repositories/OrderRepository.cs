using System;
using System.Collections.Generic;
using System.Linq;
using OA_DataAccess.EF;
using OA_DataAccess.Entities;
using OA_DataAccess.Interfaces;

namespace OA_DataAccess.Repositories
{
    /// <summary>
    /// Class repository for work with orders.
    /// </summary>
    public class OrderRepository : IOrderRepository<Order>
    {
        private readonly EShopContext _db;

        public OrderRepository(EShopContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Method for get all orders from db.
        /// </summary>
        /// <returns>list of all orders.</returns>
        public IEnumerable<Order> GetAll()
        {
            return _db.Orders.ToList();
        }

        /// <summary>
        /// Method for get order by id from db.
        /// </summary>
        /// <param name="id">id of order.</param>
        /// <returns>get order.</returns>
        public Order GetById(int id)
        {
            return _db.Orders.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Method for create order in db.
        /// </summary>
        /// <param name="order">new order.</param>
        public void Create(Order order)
        {
            order.Id = _db.Orders.Count > 0 ? _db.Orders.Max(x => x.Id) + 1 : 1;
            _db.Orders.Add(order);
        }

        /// <summary>
        /// Method for update order in db.
        /// </summary>
        /// <param name="order">updated order.</param>
        public void Update(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            var index = _db.Orders.FindIndex(x => x.Id == order.Id);
            if (index < 0)
            {
                IndexOutOfRangeException indexOutOfRangeException = new IndexOutOfRangeException("order");
                throw indexOutOfRangeException;
            }
            _db.Orders.RemoveAt(index);
            _db.Orders.Add(order);
        }

        /// <summary>
        /// Method for get current users order from db.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>list of current users orders.</returns>
        public List<Order> CurrentUsersOrders(int userId)
        {
            return _db.Orders.Where(x => x.UserId == userId).ToList();
        }
    }
}
