using System;
using System.Linq;
using OA_Repository.DTO;
using OA_UI.Controllers;

namespace OA_UI
{
    /// <summary>
    /// Class for printing console menu.
    /// </summary>
    public class Printer
    {
        /// <summary>
        /// Method for show menu options.
        /// </summary>
        public void ShowMenu()
        {
            var userController = new UserController();
            var user = new UserDTO();
            var productController = new ProductController(user);
            var orderController = new OrderController(user);
            while (true)
            {
                Console.WriteLine("\nStore MENU:");
                userController.ProcessHandler -= ProcessHandler;
                userController.ProcessHandler += ProcessHandler;
                productController.ProcessHandler -= ProcessHandler;
                productController.ProcessHandler += ProcessHandler;
                orderController.ProcessHandler -= ProcessHandler;
                orderController.ProcessHandler += ProcessHandler;
                foreach (int command in Enum.GetValues(typeof(CommandEnums)))
                {
                    Console.WriteLine($"{command} - {Enum.GetName(typeof(CommandEnums), command)}");
                }
                Console.WriteLine("Please, choose one:");
                try
                {
                    var input = Console.ReadLine();
                    if (!(input ?? throw new InvalidOperationException()).All(char.IsDigit))
                    {
                        Console.WriteLine("Only digits from list are accepted");
                        continue;
                    }
                    var command = Convert.ToInt32(input);
                    if (!Enum.IsDefined(typeof(CommandEnums), command))
                    {
                        Console.WriteLine("There is no such menu command");
                    }
                    switch (command)
                    {
                        case (int)CommandEnums.Products:
                            {
                                var items = productController.GetAllProducts();
                                foreach (var item in items)
                                {
                                    Console.WriteLine(item);
                                }
                                continue;
                            }
                        case (int)CommandEnums.FindProduct:
                            {
                                Console.Write("Enter a product's name ");
                                var items = productController.GetProductByName(Console.ReadLine());
                                foreach (var item in items)
                                {
                                    Console.WriteLine(item);
                                }
                                continue;
                            }
                        case (int)CommandEnums.Register:
                            {
                                user = userController.Register();
                                productController = new ProductController(user);
                                orderController = new OrderController(user);
                                userController.ProcessHandler -= ProcessHandler;
                                userController.ProcessHandler += ProcessHandler;
                                productController.ProcessHandler -= ProcessHandler;
                                productController.ProcessHandler += ProcessHandler;
                                orderController.ProcessHandler -= ProcessHandler;
                                orderController.ProcessHandler += ProcessHandler;
                                continue;
                            }
                        case (int)CommandEnums.Login:
                            {
                                user = userController.Login();
                                productController = new ProductController(user);
                                orderController = new OrderController(user);
                                userController.ProcessHandler -= ProcessHandler;
                                userController.ProcessHandler += ProcessHandler;
                                productController.ProcessHandler -= ProcessHandler;
                                productController.ProcessHandler += ProcessHandler;
                                orderController.ProcessHandler -= ProcessHandler;
                                orderController.ProcessHandler += ProcessHandler;
                                continue;
                            }
                        case (int)CommandEnums.Logout:
                            {
                                user = userController.Logout();
                                continue;
                            }
                        case (int)CommandEnums.MakeOrder:
                            {
                                orderController.CreateNewOrder();
                                continue;
                            }
                        case (int)CommandEnums.OrderHistory:
                            {
                                var orders = orderController.ViewOrderHistory();
                                foreach (var item in orders)
                                {
                                    Console.WriteLine(item);
                                }
                                continue;
                            }
                        case (int)CommandEnums.ChangeMyOrderStatus:
                            {
                                orderController.UserChangeOrderStatus();
                                continue;
                            }
                        case (int)CommandEnums.ChangeUsersOrdersStatus:
                            {
                                orderController.AdminChangeOrderStatus();
                                continue;
                            }
                        case (int)CommandEnums.ChangePersonalData:
                            {
                                userController.ChangePersonalData();
                                continue;
                            }
                        case (int)CommandEnums.ChangeUserData:
                            {
                                userController.ChangeUserData();
                                continue;
                            }
                        case (int)CommandEnums.AddNewProduct:
                            {
                                productController.AddNewProduct();
                                continue;
                            }
                        case (int)CommandEnums.EditProductData:
                            {
                                productController.ChangeProductData();
                                continue;
                            }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Incorrect input, please try again");
                }
            }
        }

        /// <summary>
        /// Method for handle events log.
        /// </summary>
        /// <param name="sender">object of sender info.</param>
        /// <param name="eventArgs">receive an event.</param>
        public void ProcessHandler(object sender, ProcessEventArgs eventArgs)
        {
            Console.WriteLine("Process " + (eventArgs.IsSuccessful ? "Completed Successfully" : $"Failed: {eventArgs.ErrorMessage}"));
        }
    }
}
