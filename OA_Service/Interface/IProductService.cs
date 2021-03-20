using System.Collections;
using System.Collections.Generic;
using OA_Repository.DTO;

namespace OA_Service.Interface
{
    /// <summary>
    /// Interface for getting methods from product service.
    /// </summary>
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetAllProducts();
        ProductDTO GetProductById(int id);
        ProductDTO CreateProduct(ProductDTO item);
        void UpdateProduct(int id, ProductDTO product);
        IEnumerable GetProductsByName(string name);
    }
}
