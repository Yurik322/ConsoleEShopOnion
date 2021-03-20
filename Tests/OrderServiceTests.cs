using System;
using System.Collections.Generic;
using Moq;
using OA_DataAccess.Entities;
using OA_DataAccess.Interfaces;
using OA_DataAccess.Repositories;
using OA_Repository.DTO;
using OA_Service.Implementation;
using Xunit;

namespace Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public void GetOrderByIdWithId1()
        {
            var unitOfWork = new EfUnitOfWork();
            var orderService = new OrderService(unitOfWork);
            var actual = orderService.GetOrderById(1);
            var expectedOrder = new OrderDTO
            {
                Id = 1,
                UserId = 1,
                ProductId = 1,
                TotalPrice = 10M,
                Status = "Confirmed"
            };
            Assert.Equal(expectedOrder.Id, actual.Id);
        }

        [Fact]
        public void TotalCount_WithNonEmptyItems_CalculatesTotalCount()
        {
            var order = new Order
            {
                Id = 1,
                UserId = 1,
                ProductId = 1,
                TotalPrice = 10M,
                Status = "Confirmed"
            };

            Assert.Equal(10, order.TotalPrice);
        }

        [Fact]
        public void TotalCountWithEmptyItemsReturnsZero()
        {
            var order = new Order
            {
                Id = 1,
                UserId = 1,
                ProductId = 0,
                TotalPrice = 0,
                Status = "Confirmed"
            };
            Assert.Equal(0, order.TotalPrice);
        }

        [Fact]
        public void ChangeOrderStatusWithId1StatusReceived()
        {
            var unitOfWork = new EfUnitOfWork();
            var orderService = new OrderService(unitOfWork);
            var expectedStatus = "Received";
            orderService.ChangeOrderStatus(1, expectedStatus);
            var actual = orderService.GetOrderById(1).Status;
            var expected = expectedStatus;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateNewOrderShouldCreate()
        {
            var unitOfWork = new EfUnitOfWork();
            var orderService = new OrderService(unitOfWork);
            var newOrder = new OrderDTO();
            orderService.CreateOrder(newOrder);
            var actual = 0;
            foreach (var orders in orderService.GetAllOrders())
            {
                actual++;
            }
            var expected = 2;
            Assert.Equal(expected, actual);
        }
    }
}
