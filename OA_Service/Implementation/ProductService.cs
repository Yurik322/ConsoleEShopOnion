using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OA_DataAccess.Entities;
using OA_DataAccess.Interfaces;
using OA_Repository.DTO;
using OA_Service.Interface;

namespace OA_Service.Implementation
{
    /// <summary>
    /// Class with product services.
    /// </summary>
    public class ProductService : IProductService
    {
        private IUnitOfWorks Db { get; }
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWorks unitOfWorks)
        {
            Db = unitOfWorks;
            _mapper = new MapperConfiguration(configure =>
            {
                configure.CreateMap<Product, ProductDTO>();
                configure.CreateMap<ProductDTO, Product>();
            }).CreateMapper();
        }

        /// <summary>
        /// Method for get all ProductDTO objects.
        /// </summary>
        /// <returns>collection of ProductDTO.</returns>
        public IEnumerable<ProductDTO> GetAllProducts()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(Db.Products.GetAll());
        }

        /// <summary>
        /// Method for get ProductDTO object by id.
        /// </summary>
        /// <param name="id">id of ProductDTO.</param>
        /// <returns>ProductDTO object.</returns>
        public ProductDTO GetProductById(int id)
        {
            var product = _mapper.Map<Product, ProductDTO>(Db.Products.GetById(id));
            if (product is null)
            {
                throw new ArgumentException("No Product with such Id", nameof(id));
            }
            return product;
        }

        /// <summary>
        /// Method for create new ProductDTO object.
        /// </summary>
        /// <param name="item">ProductDTO object.</param>
        /// <returns>new ProductDTO object.</returns>
        public ProductDTO CreateProduct(ProductDTO item)
        {
            var product = new ProductDTO
            {
                Name = item.Name,
                Category = item.Category,
                Description = item.Description,
                Price = item.Price
            };
            Db.Products.Create(_mapper.Map<ProductDTO, Product>(product));
            return product;
        }

        /// <summary>
        /// Method for update ProductDTO object.
        /// </summary>
        /// <param name="id">id of ProductDTO.</param>
        /// <param name="product">ProductDTO object.</param>
        public void UpdateProduct(int id, ProductDTO product)
        {
            var productFromRepository = _mapper.Map<Product, ProductDTO>(Db.Products.GetById(id));
            if (productFromRepository == null)
            {
                throw new ArgumentException("Incorrect data");
            }
            productFromRepository.Id = product.Id;
            productFromRepository.Name = product.Name;
            Db.Products.Update(_mapper.Map<ProductDTO, Product>(product));
        }

        /// <summary>
        /// Method for get list of ProductDTO objects by name.
        /// </summary>
        /// <param name="name">name of product.</param>
        /// <returns>list of ProductDTO objects.</returns>
        public IEnumerable GetProductsByName(string name)
        {
            var inputName = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(Db.Products.GetAll());
            if (!string.IsNullOrEmpty(name))
            {
                inputName = inputName.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }
            return inputName.ToList();
        }
    }
}
