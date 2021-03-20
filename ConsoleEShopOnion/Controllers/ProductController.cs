using System;
using System.Collections.Generic;
using OA_DataAccess.Entities;
using OA_DataAccess.Repositories;
using OA_Repository.DTO;
using OA_Service.Implementation;

namespace OA_UI.Controllers
{
    /// <summary>
    /// Class controller for product services.
    /// </summary>
    public class ProductController
    {
        private readonly EfUnitOfWork _unitOfWork = new EfUnitOfWork();
        private readonly ProductService _productService;
        public event EventHandler<ProcessEventArgs> ProcessHandler;
        private readonly UserDTO _currentUser;
        public ProductController(UserDTO currentUser)
        {
            _currentUser = currentUser;
            _productService = new ProductService(_unitOfWork);
        }

        /// <summary>
        /// Method for get list of all products.
        /// </summary>
        /// <returns>list of products.</returns>
        public List<string> GetAllProducts()
        {
            var products = new List<string>();
            foreach (var product in _productService.GetAllProducts())
            {
                products.Add(product.ToString());
            }
            return products;
        }

        /// <summary>
        /// Method for get list of product by name.
        /// </summary>
        /// <param name="name">name of product.</param>
        /// <returns>list of products.</returns>
        public List<string> GetProductByName(string name)
        {
            var products = new List<string>();
            try
            {
                foreach (var product in _productService.GetProductsByName(name))
                {
                    products.Add(product.ToString());
                }
            }
            catch
            {
                OnProcessCompleted(new ProcessEventArgs("There is no such product"));
            }
            return products;
        }

        /// <summary>
        /// Method for create new product.
        /// </summary>
        public void AddNewProduct()
        {
            if (_currentUser.Role < Roles.Admin)
            {
                OnProcessCompleted(new ProcessEventArgs("You must logout to register a new account"));
                return;
            }
            try
            {
                var product = new ProductDTO();
                Console.WriteLine("Enter product Name:");
                product.Name = Console.ReadLine();
                Console.WriteLine("Enter Category:");
                product.Category = Console.ReadLine();
                Console.WriteLine("Enter Description:");
                product.Description = Console.ReadLine();
                Console.WriteLine("Enter Price:");
                product.Price = Convert.ToDecimal(Console.ReadLine());
                _productService.CreateProduct(product);
                OnProcessCompleted(new ProcessEventArgs());
            }
            catch
            {
                OnProcessCompleted(new ProcessEventArgs("Incorrect input"));
            }
        }

        /// <summary>
        /// Method for update product data.
        /// </summary>
        public void ChangeProductData()
        {
            if (_currentUser.Role < Roles.Admin)
            {
                OnProcessCompleted(new ProcessEventArgs("You are not permitted to use this command"));
                return;
            }
            try
            {
                Console.WriteLine("Enter id of product You want to change: ");
                var id = Console.ReadLine();
                var product = _productService.GetProductById(Convert.ToInt32(id));
                GetProductById(id);
                Console.WriteLine("Enter new product Name:");
                product.Name = Console.ReadLine();
                Console.WriteLine("Enter new Category:");
                product.Category = Console.ReadLine();
                Console.WriteLine("Enter new Description:");
                product.Description = Console.ReadLine();
                Console.WriteLine("Enter new Price:");
                product.Price = Convert.ToDecimal(Console.ReadLine());
                _productService.UpdateProduct(product.Id, product);
                OnProcessCompleted(new ProcessEventArgs());
            }
            catch
            {
                OnProcessCompleted(new ProcessEventArgs("Incorrect input"));
            }
        }

        /// <summary>
        /// Method for get product by id.
        /// </summary>
        /// <param name="id">id of product.</param>
        public void GetProductById(string id)
        {
            if (_currentUser.Role < Roles.Admin)
            {
                OnProcessCompleted(new ProcessEventArgs("You are not permitted to use this command"));
                return;
            }
            try
            {
                var product = _productService.GetProductById(Convert.ToInt32(id));
                Console.WriteLine("Product information:");
                Console.WriteLine(product.ToString());
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
