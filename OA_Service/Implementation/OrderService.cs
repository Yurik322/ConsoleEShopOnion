using System;
using System.Collections.Generic;
using AutoMapper;
using OA_DataAccess.Entities;
using OA_DataAccess.Interfaces;
using OA_Repository.DTO;
using OA_Service.Interface;

namespace OA_Service.Implementation
{
    /// <summary>
    /// Class with order services.
    /// </summary>
    public class OrderService : IOrderService
    {
        private IUnitOfWorks Db { get; }
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWorks unitOfWorks)
        {
            Db = unitOfWorks;
            _mapper = new MapperConfiguration(configure =>
            {
                configure.CreateMap<Order, OrderDTO>();
                configure.CreateMap<OrderDTO, Order>();
            }).CreateMapper();
        }

        /// <summary>
        /// Method for get all OrderDTO objects.
        /// </summary>
        /// <returns>collection of OrderDTO.</returns>
        public IEnumerable<OrderDTO> GetAllOrders()
        {
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(Db.Orders.GetAll());
        }

        /// <summary>
        /// Method for get OrderDTO object by id.
        /// </summary>
        /// <param name="id">id of OrderDTO.</param>
        /// <returns>OrderDTO object.</returns>
        public OrderDTO GetOrderById(int id)
        {
            var orders = _mapper.Map<Order, OrderDTO>(Db.Orders.GetById(id));
            if (orders is null)
            {
                throw new ArgumentException("No Order with such Id", nameof(id));
            }
            return orders;
        }

        /// <summary>
        /// Method for create new OrderDTO object.
        /// </summary>
        /// <param name="order">OrderDTO object.</param>
        public void CreateOrder(OrderDTO order)
        {
            Db.Orders.Create(_mapper.Map<OrderDTO, Order>(order));
        }

        /// <summary>
        /// Method for get all current users OrderDTO objects.
        /// </summary>
        /// <param name="id">id of OrderDTO object.</param>
        /// <returns>list of current users OrderDTO objects.</returns>
        public List<OrderDTO> ShowCurrentUsersOrders(int id)
        {
            return _mapper.Map<List<Order>, List<OrderDTO>>(Db.Orders.CurrentUsersOrders(id));
        }

        /// <summary>
        /// Method for change status of users OrderDTO object.
        /// </summary>
        /// <param name="id">id of OrderDTO object.</param>
        /// <param name="status">status of OrderDTO object.</param>
        public void ChangeOrderStatus(int id, string status)
        {
            var orderFromRepository = _mapper.Map<Order, OrderDTO>(Db.Orders.GetById(id));
            if (orderFromRepository == null)
            {
                throw new ArgumentException("Null status");
            }
            orderFromRepository.Status = status;
            Db.Orders.Update(_mapper.Map<OrderDTO, Order>(orderFromRepository));
        }
    }
}
