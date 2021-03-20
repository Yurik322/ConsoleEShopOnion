using System;
using System.Collections.Generic;
using System.Linq;
using OA_DataAccess.EF;
using OA_DataAccess.Entities;
using OA_DataAccess.Interfaces;

namespace OA_DataAccess.Repositories
{
    /// <summary>
    /// Class repository for work with products.
    /// </summary>
    public class ProductRepository : IRepository<Product>
    {
        private readonly EShopContext _db;

        public ProductRepository(EShopContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Method for get all products from db.
        /// </summary>
        /// <returns>list of all products.</returns>
        public IEnumerable<Product> GetAll()
        {
            return _db.Products.ToList();
        }

        /// <summary>
        /// Method for get product by id from db.
        /// </summary>
        /// <param name="id">id of product.</param>
        /// <returns>get product.</returns>
        public Product GetById(int id)
        {
            return _db.Products.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Method for create product in db.
        /// </summary>
        /// <param name="product">new product.</param>
        public void Create(Product product)
        {
            product.Id = _db.Products.Count > 0 ? _db.Products.Max(x => x.Id) + 1 : 1;
            _db.Products.Add(product);
        }

        /// <summary>
        /// Method for update product in db.
        /// </summary>
        /// <param name="product">updated product.</param>
        public void Update(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            var index = _db.Products.FindIndex(x => x.Id == product.Id);
            if (index < 0)
            {
                IndexOutOfRangeException indexOutOfRangeException = new IndexOutOfRangeException("product");
                throw indexOutOfRangeException;
            }
            _db.Products.RemoveAt(index);
            _db.Products.Add(product);
        }
    }
}
