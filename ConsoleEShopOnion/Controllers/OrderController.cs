using System;
using System.Collections.Generic;
using OA_DataAccess.Entities;
using OA_DataAccess.Repositories;
using OA_Repository.DTO;
using OA_Service.Implementation;

namespace OA_UI.Controllers
{
    /// <summary>
    /// Class controller for order services.
    /// </summary>
    public class OrderController
    {
        private readonly EfUnitOfWork _unitOfWork = new EfUnitOfWork();
        private readonly ProductService _productService;
        private readonly OrderService _orderService;
        public event EventHandler<ProcessEventArgs> ProcessHandler;
        private readonly UserDTO _currentUser;
        public OrderController(UserDTO currentUser)
        {
            _currentUser = currentUser;
            _productService = new ProductService(_unitOfWork);
            _orderService = new OrderService(_unitOfWork);
        }

        /// <summary>
        /// Method for create new order.
        /// </summary>
        public void CreateNewOrder()
        {
            if (_currentUser.Role < Roles.Registered)
            {
                OnProcessCompleted(new ProcessEventArgs("You are not permitted to use this command"));
                return;
            }
            try
            {
                Console.WriteLine("Enter id of item to create new order: ");
                var id = Convert.ToInt32(Console.ReadLine());
                var product = _productService.GetProductById(id);
                if (product is null)
                {
                    OnProcessCompleted(new ProcessEventArgs("There is no product with such id"));
                }
                else
                {
                    var order = new OrderDTO
                    {
                        UserId = _currentUser.Id,
                        ProductId = product.Id,
                        Status = "New",
                        TotalPrice = product.Price
                    };
                    _orderService.CreateOrder(order);
                    OnProcessCompleted(new ProcessEventArgs());
                }
            }
            catch
            {
                OnProcessCompleted(new ProcessEventArgs("There is no product with such id"));
            }
        }

        /// <summary>
        /// Method for get order history.
        /// </summary>
        /// <returns>list of user orders.</returns>
        public List<string> ViewOrderHistory()
        {
            var order = new List<string>();
            if (_currentUser.Role < Roles.Registered)
            {
                order.Add("You are not permitted to use this command");
                return order;
            }
            var orders = _orderService.ShowCurrentUsersOrders(_currentUser.Id);
            foreach (var product in orders)
            {
                order.Add(product.ToString());
            }
            OnProcessCompleted(new ProcessEventArgs());
            return order;
        }

        /// <summary>
        /// Method for change status of order by administrator.
        /// </summary>
        public void AdminChangeOrderStatus()
        {
            if (_currentUser.Role < Roles.Admin)
            {
                OnProcessCompleted(new ProcessEventArgs("You are not permitted to use this command"));
                return;
            }
            try
            {
                Console.WriteLine("Enter Order id:");
                var id = Convert.ToInt32(Console.ReadLine());
                GetOrderById(id);
                Console.WriteLine("Choose Order status: \n" +
                                  "1 - New\n" +
                                  "2 - Canceled by Admin\n" +
                                  "3 - Payment Received\n" +
                                  "4 - Sent\n" +
                                  "5 - Received\n" +
                                  "6 - Complete");
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        _orderService.ChangeOrderStatus(id, "New");
                        break;
                    case 2:
                        _orderService.ChangeOrderStatus(id, "Canceled by Admin");
                        break;
                    case 3:
                        _orderService.ChangeOrderStatus(id, "Payment Received");
                        break;
                    case 4:
                        _orderService.ChangeOrderStatus(id, "Sent");
                        break;
                    case 5:
                        _orderService.ChangeOrderStatus(id, "Received");
                        break;
                    case 6:
                        _orderService.ChangeOrderStatus(id, "Complete");
                        break;
                }
                OnProcessCompleted(new ProcessEventArgs());
            }
            catch
            {
                OnProcessCompleted(new ProcessEventArgs("Something went wrong"));
            }
        }

        /// <summary>
        /// Method for change status of order by registered user.
        /// </summary>
        public void UserChangeOrderStatus()
        {
            if (_currentUser.Role < Roles.Registered)
            {
                OnProcessCompleted(new ProcessEventArgs("You are not permitted to use this command"));
                return;
            }
            try
            {
                Console.WriteLine("Enter Order id:");
                var id = Convert.ToInt32(Console.ReadLine());
                if (_orderService.GetOrderById(id) is null || _orderService.GetOrderById(id).UserId != _currentUser.Id)
                {
                    Console.WriteLine("You don't have Order with such id");
                }
                else
                {
                    GetOrderById(id);
                    Console.WriteLine("Choose Order status: \n" +
                                      "1 - Received\n" +
                                      "2 - Canceled by Registered");
                    switch (Convert.ToInt32(Console.ReadLine()))
                    {
                        case 1:
                            _orderService.ChangeOrderStatus(id, "Received");
                            break;
                        case 2:
                            if (_orderService.GetOrderById(id).Status == "Completed" || _orderService.GetOrderById(id).Status == "Received")
                            {
                                Console.WriteLine("You cant change this status");
                            }
                            else
                            {
                                _orderService.ChangeOrderStatus(id, "Canceled by Registered");
                            }
                            break;
                    }
                    OnProcessCompleted(new ProcessEventArgs());
                }

            }
            catch
            {
                OnProcessCompleted(new ProcessEventArgs("Something went wrong"));
            }
        }

        /// <summary>
        /// Method for get order by id.
        /// </summary>
        /// <param name="id">id of order.</param>
        public void GetOrderById(int id)
        {
            if (_currentUser.Role < Roles.Admin)
            {
                OnProcessCompleted(new ProcessEventArgs("You are not permitted to use this command"));
                return;
            }
            try
            {
                var order = _orderService.GetOrderById(Convert.ToInt32(id));
                Console.WriteLine("Order information:");
                Console.WriteLine($"Id: {order.Id}, Registered Id: {order.UserId}, Status: {order.Status}, Total Price: {order.TotalPrice}");
            }
            catch
            {
                OnProcessCompleted(new ProcessEventArgs("There is no such user"));
            }
        }

        protected virtual void OnProcessCompleted(ProcessEventArgs eventArgs)
        {
            ProcessHandler?.Invoke(this, eventArgs);
        }
    }
}
