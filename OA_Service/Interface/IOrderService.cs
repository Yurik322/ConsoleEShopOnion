using System.Collections.Generic;
using OA_Repository.DTO;

namespace OA_Service.Interface
{
    /// <summary>
    /// Interface for getting methods from order service.
    /// </summary>
    public interface IOrderService
    {
        IEnumerable<OrderDTO> GetAllOrders();
        OrderDTO GetOrderById(int id);
        void CreateOrder(OrderDTO order);
        List<OrderDTO> ShowCurrentUsersOrders(int id);
        void ChangeOrderStatus(int id, string status);
    }
}
